using Microsoft.AspNetCore.Mvc;
using TestTaskWebApi.Models;
using TestTaskWebApi.Services;
using TestTaskWebApi.Services.JwtTokenService;

namespace TestTaskWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IUserService _userService;

        public AccountController(
            IJwtTokenService tokenService,
            IUserService userService)
        {
            _jwtTokenService = tokenService;
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var result  = await _userService.AuthenticateAsync(loginRequest);

            if (result.IsAuthenticated)
            {
                return Ok(result);
            }
            
           return Ok(result);
        }
    }
}
