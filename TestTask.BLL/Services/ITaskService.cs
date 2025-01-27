using TestTask.DAL.Repositories.Entities;

namespace TestTask.BLL.Services
{
    public interface ITaskService
    {
        Task<List<ToDoTaskModel>> GetAllTasksAsync(int items, int page);
        Task<ToDoTaskModel?> CreateTaskAsync(ToDoTaskModel task);
        Task<bool> UpdateTaskAsync(ToDoTaskModel task);
        Task<bool> DeleteTaskAsync(int id);
    }
}