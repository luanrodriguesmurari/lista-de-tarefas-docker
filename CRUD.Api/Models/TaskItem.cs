namespace CRUD.Api.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Completed { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;
    }
}
