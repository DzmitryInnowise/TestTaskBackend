using TestTask.DAL.Repositories.Entities;

namespace TestTask.DAL.Repositories
{
    public interface IToDoTaskRepository : IGenericRepository<ToDoTask>
    {
        Task<List<ToDoTaskModel>> GetAllAsync(int items, int page);
    }
}
