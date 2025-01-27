namespace TestTask.DAL.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IToDoTaskRepository ToDoTask { get; }
        IUserRepository User { get; }

        int Save();
    }
}
