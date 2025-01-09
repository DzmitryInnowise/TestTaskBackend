using System.Security.Claims;

namespace TestTaskWebApi.Services.JwtTokenService
{
    public interface IJwtTokenService
    {
         string CreateToken(List<Claim> claims);
    }
}
