﻿using TaskManager.Core.Entities.Base;

namespace TaskManager.Core.Entities
{
    public class User : BaseEntity
    {
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; }
        public string? Password { get; set; }
        public virtual ICollection<Task> AssingedTasks { get; set; }
        public virtual ICollection<Task> CreatedTasks { get; set; }

    }
}
