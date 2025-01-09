using Microsoft.EntityFrameworkCore;
using TestTaskWebApi.Repositories.DBContext;
using TestTaskWebApi.Repositories;
using TestTaskWebApi.Repositories.Enteties;

public class TaskRepository : ITaskRepository
{
    private readonly AppDbContext _context;

    public TaskRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<ToDoTaskModel>> GetAllAsync(int items, int page)
    {
        var response =  await _context.ToDoTasks.
            Include(t => t.Creator).
            Include(t => t.Updater).
            Skip((page)*items).
            Take(items).
            Select(task => new ToDoTaskModel
             {
                 Id = task.Id,
                 Name = task.Name,
                 IsCompleted = task.IsCompleted,
                 CreatedBy = task.Creator.Email,
                 UpdatedBy = task.Updater.Email
             })
            .ToListAsync();

        return response;
    }

    public async Task<ToDoTaskModel> AddAsync(ToDoTaskModel task, int userId)
    {
        if (task == null) throw new ArgumentNullException(nameof(task));

        var taskModel = new ToDoTask
        {
            Name = task.Name,
            IsCompleted = task.IsCompleted,
            CreatedBy = userId,
        };

        await _context.ToDoTasks.AddAsync(taskModel);
        await _context.SaveChangesAsync();
        task.Id = taskModel.Id;
        return task;
    }

    public async Task<ToDoTaskModel?> UpdateAsync(ToDoTaskModel task, int userId)
    {
        if (task == null) throw new ArgumentNullException(nameof(task));

        var existingTask = await _context.ToDoTasks.FindAsync(task.Id);
        if (existingTask == null) return null;

        existingTask.Name = task.Name;
        existingTask.IsCompleted = task.IsCompleted;
        existingTask.UpdatedBy = userId;

        await _context.SaveChangesAsync();

        return task;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var task = await _context.ToDoTasks.FindAsync(id);
        if (task == null) return false;

        _context.ToDoTasks.Remove(task);
        await _context.SaveChangesAsync();
        return true;
    }
}

