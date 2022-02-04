using System;
using System.Collections.Generic;

namespace TodoApp.DAL.Entities
{
    public sealed class Project
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public List<Todo> AttachedTodos { get; set; } = new List<Todo>();
        public List<User> ProjectMembers { get; set; } = new List<User>();
    }
}