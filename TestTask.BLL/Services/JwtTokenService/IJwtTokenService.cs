using System.Security.Claims;

namespace TestTask.BLL.Services.JwtTokenService
{
    public interface IJwtTokenService
    {
         string CreateToken(List<Claim> claims);
    }
}
