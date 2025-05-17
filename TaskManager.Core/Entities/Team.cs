using TaskManager.Core.Entities.Base;

namespace TaskManager.Core.Entities
{
    public class Team : BaseEntity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
