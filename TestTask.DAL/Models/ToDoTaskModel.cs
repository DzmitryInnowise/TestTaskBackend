namespace TestTask.DAL.Repositories.Entities
{
    public class ToDoTaskModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public bool IsCompleted { get; set; }
        public required string CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
