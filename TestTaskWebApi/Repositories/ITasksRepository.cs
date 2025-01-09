using TestTaskWebApi.Repositories.Enteties;

namespace TestTaskWebApi.Repositories
{
    public interface ITaskRepository
    {
        Task<List<ToDoTaskModel>> GetAllAsync(int items, int page);
        Task<ToDoTaskModel> AddAsync(ToDoTaskModel task, int userId);
        Task<ToDoTaskModel?> UpdateAsync(ToDoTaskModel task, int userId);
        Task<bool> DeleteAsync(int id);
    }
}
