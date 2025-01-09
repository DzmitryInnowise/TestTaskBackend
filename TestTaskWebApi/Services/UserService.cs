using System.Security.Claims;
using TestTaskWebApi.Models;
using TestTaskWebApi.Repositories;
using TestTaskWebApi.Services.JwtTokenService;

namespace TestTaskWebApi.Services
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenService _jwtTokenService;

        public UserService(
            IUserRepository userRepository,
            IJwtTokenService jwtTokenService)
        {
            _userRepository = userRepository;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<AuthenticateResponse> AuthenticateAsync(LoginRequest loginRequest)
        {         
            var user = await _userRepository.GetUserByEmailAsync(loginRequest.Email);

            var authenticateResponse = new AuthenticateResponse();

            if (user == null) 
            {
                authenticateResponse.ErrorMessage = $"The user with email {loginRequest.Email} wasn't found";
                // No such user
                return authenticateResponse;
            }

            if (!VerifyPassword(loginRequest.Password, user!.Password))
            {
                authenticateResponse.ErrorMessage = $"Wrong credentials";
                // Wrong credentials
                return authenticateResponse; 
            }

            // Generate JWT token
            var claims = new List<Claim>
            {
                new Claim("Email", user.Email)
            };

            authenticateResponse.Token = _jwtTokenService.CreateToken(claims);
            authenticateResponse.IsAuthenticated = true;

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
