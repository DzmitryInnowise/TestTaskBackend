using TestTaskWebApi.Repositories.Enteties;

namespace TestTaskWebApi.Services
{
    public interface ITaskService
    {
        Task<List<ToDoTaskModel>> GetAllTasksAsync(int items, int page);
        Task<ToDoTaskModel?> CreateTaskAsync(ToDoTaskModel task);
        Task<ToDoTaskModel?> UpdateTaskAsync(int id, ToDoTaskModel task);
        Task<bool> DeleteTaskAsync(int id);
    }
}