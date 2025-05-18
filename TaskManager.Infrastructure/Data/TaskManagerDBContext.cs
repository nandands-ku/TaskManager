using Microsoft.EntityFrameworkCore;
using System.Data;
using TaskManager.Core.Entities;
using TaskManager.Core.Enum;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasData(
                new User { Id = Guid.Parse("d44f8bb4-5c2a-4c2f-b3c3-1dffb739fed0"), FullName = "Admin", Email = "admin@demo.com", Password = "Admin123!", Role = "Admin" }
            );
        }
    }
}
