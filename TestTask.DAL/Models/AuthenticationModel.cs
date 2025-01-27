namespace TestTask.DAL.Models
{
    public class AuthorizationModel
    {
        public bool IsAuthorized { get; set; } = false;
        public string? ErrorMessage { get; set; }
        public string? Token { get; set; }
    }
}
