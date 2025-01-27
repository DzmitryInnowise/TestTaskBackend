using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestTask.DAL.Repositories.Entities;
using TestTask.BLL.Services;

namespace TestTaskWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ToDoTasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public ToDoTasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        /// <summary>
        /// Retrieves a paginated list of tasks.
        /// </summary>
        /// <param name="items">Number of items per page.</param>
        /// <param name="page">Page number.</param>
        /// <returns>
        /// - 200 OK: Returns a list of tasks.
        /// - 204 No Content: No tasks available.
        /// - 400 Bad Request: Invalid query parameters.
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> GetTasks(int items, int page)
        {
            if (items <= 0 || page < 0)
            {
                return BadRequest(new { message = "Invalid query parameters. Items and page must be positive." });
            }

            var toDoTasks = await _taskService.GetAllTasksAsync(items, page);
            if (toDoTasks == null || toDoTasks.Count == 0)
            {
                return NoContent(); 
            }
            return Ok(toDoTasks);
        }

        /// <summary>
        /// Creates a new task in the system.
        /// </summary>
        /// <param name="toDoTask">The task object to be created.</param>
        /// <returns>
        /// - 201 Created: Returns the created task.
        /// - 400 Bad Request: Task data is invalid or missing required fields.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] ToDoTaskModel toDoTask)
        {
            if (toDoTask == null || string.IsNullOrEmpty(toDoTask.Name))
            {
                return BadRequest(new { message = "ToDoTask is null or data is invalid." }); 
            }

            var createdTask = await _taskService.CreateTaskAsync(toDoTask);

            if (createdTask == null)
            {
                return BadRequest(new { message = "Wasn't able to create new ToDoTask" });
            }

            return CreatedAtAction(nameof(CreateTask), new { id = createdTask.Id }, createdTask);
        }

        /// <summary>
        /// Updates an existing task by its ID.
        /// </summary>
        /// <param name="id">The unique identifier of the task to update.</param>
        /// <param name="toDoTask">The updated task object.</param>
        /// <returns>
        /// - 200 OK: Returns the updated task.
        /// - 400 Bad Request: Task ID in the route does not match the request body.
        /// - 404 Not Found: Task with the specified ID does not exist.
        /// </returns>
        [HttpPut("{toDoTaskId}")]
        public async Task<IActionResult> UpdateTask(int toDoTaskId, [FromBody] ToDoTaskModel toDoTask)
        {
            if (toDoTaskId != toDoTask.Id)
            {
                return BadRequest(new { message = "Task ID in the route does not match the request body." });
            }

            var updatedToDoTask = await _taskService.UpdateTaskAsync(toDoTask);
            if (!updatedToDoTask)
            {
                return NotFound(new { message = $"Task with ID {toDoTaskId} not found." }); 
            }

            return Ok(updatedToDoTask); 
        }

        /// <summary>
        /// Deletes a specific task by its ID.
        /// </summary>
        /// <param name="toDoTaskId">The unique identifier of the task to delete.</param>
        /// <returns>
        /// - 200 OK: Confirms that the task has been deleted.
        /// - 404 Not Found: Task with the specified ID does not exist.
        /// </returns>
        [HttpDelete("{toDoTaskId}")]
        public async Task<IActionResult> DeleteTask(int toDoTaskId)
        {
            var isDeleted = await _taskService.DeleteTaskAsync(toDoTaskId);
            if (!isDeleted)
            {
                return NotFound(new { message = $"Task with ID {toDoTaskId} not found." });
            }

            return Ok(new { message = $"Task with ID {toDoTaskId} has been deleted." }); 
        }
    }
}
