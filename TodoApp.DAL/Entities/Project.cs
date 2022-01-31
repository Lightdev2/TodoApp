using System.Collections.Generic;

namespace TodoApp.DAL.Entities
{
    public sealed class Project
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<Todo> AttachedTodos { get; set; }
    }
}