namespace TestTask.DAL.Repositories.Entities
{
    public class ToDoTask
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public bool IsCompleted { get; set; }
        public int CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public User? Creator { get; set; } 
        public User? Updater { get; set; }
    }
}
