using Microsoft.EntityFrameworkCore;
using TestTask.DAL.Repositories.DBContext;
using TestTask.DAL.Repositories.Entities;

namespace TestTask.DAL.Repositories
{
    public class ToDoTaskRepository : GenericRepository<ToDoTask>, IToDoTaskRepository
    {
        private readonly AppDbContext _context;

        public ToDoTaskRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<ToDoTaskModel>> GetAllAsync(int items, int page)
        {
            var toDoTasks = await _context.ToDoTasks.
                Include(t => t.Creator).
                Include(t => t.Updater).
                Skip(page * items).
                Take(items).
                Select(task => new ToDoTaskModel
                {
                    Id = task.Id,
                    Name = task.Name,
                    IsCompleted = task.IsCompleted,
                    CreatedBy = task.Creator.Email ?? "-",
                    UpdatedBy = task.Updater.Email ?? "-"
                })
                .AsNoTracking()
                .ToListAsync();

            return toDoTasks;
        }
    }
}

