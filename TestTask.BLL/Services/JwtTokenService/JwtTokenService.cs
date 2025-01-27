using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace TestTask.BLL.Services.JwtTokenService
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly JwtTokenSettings jwtSettings;
        private readonly JwtSecurityTokenHandler _tokenHandler;

        private TimeSpan timeInterval = TimeSpan.FromMinutes(5);

        public JwtTokenService(IOptions<JwtTokenSettings> JwtTokenSettings)
        {
            jwtSettings = JwtTokenSettings.Value;
            _tokenHandler = new JwtSecurityTokenHandler();
        }

        /// <summary>
        /// Create JWT Token.
        /// </summary>
        /// <param name="claims">User main info.</param>
        /// <returns>JW Token.</returns>
        public string CreateToken(List<Claim> claims)
        {
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = jwtSettings.JwtAudience,
                IssuedAt = DateTime.UtcNow,
                Issuer = jwtSettings.JwtIssuer,
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(timeInterval),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.JwtSecretKey)), SecurityAlgorithms.HmacSha256),
            };

            var token = _tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            var jwt = _tokenHandler.WriteToken(token);
            return jwt;
        }
    }
}
