using TestTaskWebApi.Repositories;
using TestTaskWebApi.Repositories.Enteties;

namespace TestTaskWebApi.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IUserRepository _userRepository;

        public TaskService(
            ITaskRepository taskRepository,
            IUserRepository userRepository)
        {
            _taskRepository = taskRepository;
            _userRepository = userRepository;
        }

        public async Task<List<ToDoTaskModel>> GetAllTasksAsync(int items, int page)
        {
            return await _taskRepository.GetAllAsync(items, page);
        }

        public async Task<ToDoTaskModel?> CreateTaskAsync(ToDoTaskModel task)
        {
            var user = await _userRepository.GetUserByEmailAsync(task.CreatedBy);

            if (user == null)
            {
                // Add error handling
                return null;
            }

            return await _taskRepository.AddAsync(task, user.Id);
        }

        public async Task<ToDoTaskModel?> UpdateTaskAsync(int id, ToDoTaskModel task)
        {
            var user = await _userRepository.GetUserByEmailAsync(task.UpdatedBy!);

            if (user == null)
            {
                // Add error handling
                return null;
            }
            return await _taskRepository.UpdateAsync(task, user.Id);
        }

        public async Task<bool> DeleteTaskAsync(int id)
        {
            return await _taskRepository.DeleteAsync(id);
        }
    }
}
