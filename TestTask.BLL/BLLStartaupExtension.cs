using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestTask.BLL.Services.JwtTokenService;
using TestTask.BLL.Services;
using TestTask.DAL;

namespace TestTask.BLL
{
    public static class BLLStartaupExtension
    {
        public static void AddBLLServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Add services to the container using the extension method
            services.AddDALServices(configuration);

            // Add scoped dependencies
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IJwtTokenService, JwtTokenService>();
            services.AddScoped<ITaskService, TaskService>();
        }
    }
}
