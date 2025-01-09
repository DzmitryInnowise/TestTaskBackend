using System.ComponentModel.DataAnnotations;

namespace TestTaskWebApi.Repositories.Enteties
{
    public class User
    {
        public required int Id { get; set; }
        [Required]
        [EmailAddress]
        public required string Email { get; set; }
        public required string Password { get; set; }
        public ICollection<ToDoTask> CreatedTasks { get; set; }
        public ICollection<ToDoTask> UpdatedTasks { get; set; }
    }
}
