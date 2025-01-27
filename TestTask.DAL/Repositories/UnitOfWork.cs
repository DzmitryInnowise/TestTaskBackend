using TestTask.DAL.Repositories.DBContext;

namespace TestTask.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;
        public IToDoTaskRepository ToDoTask { get; }
        public IUserRepository User { get; }

        public UnitOfWork(
            AppDbContext dbContext,
            IUserRepository userRepository,
            IToDoTaskRepository taskRepository)
        {
            _dbContext = dbContext;
            ToDoTask = taskRepository;
            User = userRepository;
        }

        public int Save()
        {
            return _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
        }
    }
}
