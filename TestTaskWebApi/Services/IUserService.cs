using TestTaskWebApi.Models;

namespace TestTaskWebApi.Services
{
    public interface IUserService
    {
        Task<AuthenticateResponse> AuthenticateAsync(LoginRequest loginRequest);
    }
}