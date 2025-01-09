using Microsoft.EntityFrameworkCore;
using TestTaskWebApi.Repositories.DBContext;
using TestTaskWebApi.Repositories.Enteties;

namespace TestTaskWebApi.Repositories
{
    public class UserRepository: IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
        }
    }
}
