using TaskManager.Core.Entities.Base;

namespace TaskManager.Core.Entities
{
    public class Task : BaseEntity
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
        public Guid AssignedToUserId { get; set; }
        public Guid CreatedByUserId { get; set; }
        public Guid TeamId { get; set; }
        public DateTime DueDate { get; set; }
    }
}
