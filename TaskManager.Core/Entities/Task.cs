using System.ComponentModel.DataAnnotations.Schema;
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

        [ForeignKey(nameof(TeamId))]
        public virtual Team Team { get; set; } = default!;
        [ForeignKey(nameof(CreatedByUserId))]
        public virtual User CreatedByUser { get; set; } = default!;
        [ForeignKey(nameof(AssignedToUserId))]
        public virtual User AssignedToUser { get; set; } = default!;

    }
}
