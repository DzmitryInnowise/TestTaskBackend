namespace TestTaskWebApi.Models
{
    public class AuthenticateResponse
    {
        public bool IsAuthenticated { get; set; } = false;
        public string? ErrorMessage { get; set; }
        public string? Token { get; set; }
    }
}
