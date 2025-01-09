using TestTaskWebApi.Repositories.Enteties;

namespace TestTaskWebApi.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetUserByEmailAsync(string username);
    }
}