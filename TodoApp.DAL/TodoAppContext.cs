using Microsoft.EntityFrameworkCore;
using TodoApp.DAL.Entities;

namespace TodoApp.DAL
{
    public class TodoAppContext : DbContext
    {
        public TodoAppContext(DbContextOptions<TodoAppContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProjectConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new TodoConfiguration());
        }
        
        public DbSet<Todo> Todos { get; set; }
        
        public DbSet<Project> Projects { get; set; }
        
        public DbSet<User> Users { get; set; }

        public DbSet<UserSession> Sessions { get; set; }
    }
}