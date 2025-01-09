using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestTaskWebApi.Repositories.Enteties;
using TestTaskWebApi.Services;

namespace TestTaskWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        /// <summary>
        /// Retrieves a list of all tasks.
        /// </summary>
        /// <returns>
        /// - 200 OK: Returns a list of tasks.
        /// - 204 No Content: No tasks exist in the database.
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> GetTasks(int items, int page)
        {
            var tasks = await _taskService.GetAllTasksAsync(items, page);
            if (tasks == null || tasks.Count == 0)
            {
                return NoContent(); 
            }
            return Ok(tasks);
        }

        /// <summary>
        /// Creates a new task in the system.
        /// </summary>
        /// <param name="task">The task object to be created.</param>
        /// <returns>
        /// - 201 Created: Returns the created task.
        /// - 400 Bad Request: Task data is invalid or missing required fields.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] ToDoTaskModel task)
        {
            if (task == null || string.IsNullOrEmpty(task.Name))
            {
                return BadRequest(new { message = "Task data is invalid." }); 
            }

            var createdTask = await _taskService.CreateTaskAsync(task);

            if (createdTask == null)
            {
                return BadRequest(new { message = "Creation failed" });
            }

            return CreatedAtAction(nameof(CreateTask), new { id = createdTask.Id }, createdTask);
        }

        /// <summary>
        /// Updates an existing task by its ID.
        /// </summary>
        /// <param name="id">The unique identifier of the task to update.</param>
        /// <param name="task">The updated task object.</param>
        /// <returns>
        /// - 200 OK: Returns the updated task.
        /// - 400 Bad Request: Task ID in the route does not match the request body.
        /// - 404 Not Found: Task with the specified ID does not exist.
        /// </returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] ToDoTaskModel task)
        {
            if (id != task.Id)
            {
                return BadRequest(new { message = "Task ID in the route does not match the request body." });
            }

            var updatedTask = await _taskService.UpdateTaskAsync(id, task);
            if (updatedTask == null)
            {
                return NotFound(new { message = $"Task with ID {id} not found." }); 
            }

            return Ok(updatedTask); 
        }

        /// <summary>
        /// Deletes a specific task by its ID.
        /// </summary>
        /// <param name="id">The unique identifier of the task to delete.</param>
        /// <returns>
        /// - 200 OK: Confirms that the task has been deleted.
        /// - 404 Not Found: Task with the specified ID does not exist.
        /// </returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var isDeleted = await _taskService.DeleteTaskAsync(id);
            if (!isDeleted)
            {
                return NotFound(new { message = $"Task with ID {id} not found." });
            }

            return Ok(new { message = $"Task with ID {id} has been deleted." }); 
        }
    }
}
