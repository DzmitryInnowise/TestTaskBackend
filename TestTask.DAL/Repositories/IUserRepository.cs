using TestTask.DAL.Repositories.Entities;

namespace TestTask.DAL.Repositories
{
    public interface IUserRepository: IGenericRepository<User>
    {
        Task<User?> GetUserByEmailAsync(string username);
    }
}