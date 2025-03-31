using Microsoft.EntityFrameworkCore;
using TaskManagementApi.Models.Domain;

namespace TaskManagementApi.Data
{
    public class TaskDbContext : DbContext
    {
        public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options) {}

        public DbSet<TaskItem> Tasks { get; set; }
    }
}