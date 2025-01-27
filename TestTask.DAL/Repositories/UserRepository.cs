using Microsoft.EntityFrameworkCore;
using TestTask.DAL.Repositories.DBContext;
using TestTask.DAL.Repositories.Entities;

namespace TestTask.DAL.Repositories
{
    public class UserRepository: GenericRepository<User>, IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
