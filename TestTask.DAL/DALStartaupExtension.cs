using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestTask.DAL.Repositories.DBContext;
using TestTask.DAL.Repositories;

namespace TestTask.DAL
{

    public static class DALStartaupExtension
    {
        public static void AddDALServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Add DbContext
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DbConnectionStrings")));

            // Add scoped dependencies
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IToDoTaskRepository, ToDoTaskRepository>();
        }
    }
}
