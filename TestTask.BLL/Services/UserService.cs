using System.Security.Claims;
using TestTask.DAL.Models;
using TestTask.DAL.Repositories;
using TestTask.BLL.Services.JwtTokenService;

namespace TestTask.BLL.Services
{
    public class UserService: IUserService
    {
        private readonly IJwtTokenService _jwtTokenService;
        public readonly IUnitOfWork _unitOfWork;

        public UserService(
            IJwtTokenService jwtTokenService,
            IUnitOfWork unitOfWork)
        {
            _jwtTokenService = jwtTokenService;
            _unitOfWork = unitOfWork;
        }

        public async Task<AuthorizationModel> AuthenticateAsync(LoginRequestModel loginRequest)
        {         
            var user = await _unitOfWork.User.GetUserByEmailAsync(loginRequest.Email);

            var authenticateResponse = new AuthorizationModel();

            if (user == null) 
            {
                authenticateResponse.ErrorMessage = $"The user with email {loginRequest.Email} wasn't found";
                // The user with email wasn't found
                return authenticateResponse;
            }

            if (!VerifyPassword(loginRequest.Password, user!.Password))
            {
                authenticateResponse.ErrorMessage = $"Wrong password. A";
                // Wrong password
                return authenticateResponse; 
            }

            // Generate JWT token
            var claims = new List<Claim>
            {
                new Claim("Email", user.Email)
            };

            authenticateResponse.Token = _jwtTokenService.CreateToken(claims);
            authenticateResponse.IsAuthorized = true;

            return authenticateResponse;
        }

        private bool VerifyPassword(string password, string storedHash)
        {
            // Store password without hashing forbidden.
            // Simple password verification logic.
            return password == storedHash;
        }
    }
}
