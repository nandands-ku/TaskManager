using Microsoft.EntityFrameworkCore;
using TaskManager.Core.Entities;
using Task = TaskManager.Core.Entities.Task;

namespace TaskManager.Infrastructure.Data
{
    public class TaskManagerDBContext : DbContext
    {
        public TaskManagerDBContext(DbContextOptions<TaskManagerDBContext> options) : base(options)
        {

        }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
