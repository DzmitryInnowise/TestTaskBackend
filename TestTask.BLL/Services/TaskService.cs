using TestTask.DAL.Repositories;
using TestTask.DAL.Repositories.Entities;

namespace TestTask.BLL.Services
{
    public class TaskService : ITaskService
    {
        public readonly IUnitOfWork _unitOfWork;

        public TaskService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<ToDoTaskModel>> GetAllTasksAsync(int items, int page)
        {
            return await _unitOfWork.ToDoTask.GetAllAsync(items, page);
        }

        public async Task<ToDoTaskModel?> CreateTaskAsync(ToDoTaskModel toDoTask)
        {
            if (toDoTask == null) throw new ArgumentNullException(nameof(toDoTask), "Task object cannot be null.");

            var user = await _unitOfWork.User.GetUserByEmailAsync(toDoTask.CreatedBy);

            if (user == null)
            {
                throw new InvalidOperationException($"User with email {toDoTask.CreatedBy} does not exist.");
            }

            var newTask = new ToDoTask
            {
                Name = toDoTask.Name,
                IsCompleted = toDoTask.IsCompleted,
                CreatedBy = user.Id,
            };

            await _unitOfWork.ToDoTask.Add(newTask);

            if (_unitOfWork.Save() > 0)
            {
                toDoTask.Id = newTask.Id;
                return toDoTask;
            }

            return null;
        }

        public async Task<bool> UpdateTaskAsync(ToDoTaskModel toDoTask)
        {
            if (toDoTask == null) throw new ArgumentNullException(nameof(toDoTask), "toDoTask object cannot be null.");

            if (string.IsNullOrEmpty(toDoTask.UpdatedBy))
            {
                throw new ArgumentException("UpdatedBy must not be null or empty.");
            }

            var user = await _unitOfWork.User.GetUserByEmailAsync(toDoTask.UpdatedBy!);

            if (user == null)
            {
                throw new InvalidOperationException($"User with email {toDoTask.UpdatedBy} does not exist.");
            }

            var existingTask = await _unitOfWork.ToDoTask.GetById(toDoTask.Id);
            if (existingTask == null)
            {
                throw new InvalidOperationException($"Task with ID {toDoTask.Id} not found.");
            }

            existingTask.Name = toDoTask.Name;
            existingTask.IsCompleted = toDoTask.IsCompleted;
            existingTask.UpdatedBy = user.Id;

            _unitOfWork.ToDoTask.Update(existingTask);

            return _unitOfWork.Save() > 0;
        }

        public async Task<bool> DeleteTaskAsync(int toDoTaskId)
        {
            var toDoTaskToDelete = await _unitOfWork.ToDoTask.GetById(toDoTaskId);

            if (toDoTaskToDelete == null)
            {
                throw new InvalidOperationException($"Task with ID {toDoTaskId} not found.");
            }

            _unitOfWork.ToDoTask.Delete(toDoTaskToDelete);

            return _unitOfWork.Save() > 0;
        }
    }
}
