using System.ComponentModel.DataAnnotations;

namespace TaskManager.Core.Entities.Base
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
    }
}
