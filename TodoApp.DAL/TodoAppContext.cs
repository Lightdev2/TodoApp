using Microsoft.EntityFrameworkCore;
using TodoApp.DAL.Entities;

namespace TodoApp.DAL
{
    public class TodoAppContext : DbContext
    {
        public TodoAppContext(DbContextOptions<TodoAppContext> options) : base(options)
        {
        }
        
        public DbSet<Todo> Todos { get; set; }
        
        public DbSet<Project> Projects { get; set; }
        
        public DbSet<User> Users { get; set; }
    }
}