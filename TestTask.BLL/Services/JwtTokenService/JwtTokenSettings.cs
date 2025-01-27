namespace TestTask.BLL.Services.JwtTokenService
{
    public class JwtTokenSettings
    {
        public string JwtIssuer { get; set; } = string.Empty;

        public string JwtAudience { get; set; } = string.Empty;

        public string JwtSecretKey { get; set; } = string.Empty;

        public int TokenLifeTime { get; set; }

        public int RefreshTokenLifeTime { get; set; }
    }
}
