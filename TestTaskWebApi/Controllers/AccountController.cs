using Microsoft.AspNetCore.Mvc;
using TestTask.DAL.Models;
using TestTask.BLL.Services;

namespace TestTaskWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(
            IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestModel loginData)
        {
            var result  = await _userService.AuthenticateAsync(loginData);

            if (result.IsAuthorized)
            {
                return Ok(result);
            }
            
           return Ok(result);
        }
    }
}
