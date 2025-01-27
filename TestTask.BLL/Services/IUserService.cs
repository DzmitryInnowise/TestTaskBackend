using Microsoft.Identity.Client;
using TestTask.DAL.Models;

namespace TestTask.BLL.Services
{
    public interface IUserService
    {
        Task<AuthorizationModel> AuthenticateAsync(LoginRequestModel loginRequest);
    }
}