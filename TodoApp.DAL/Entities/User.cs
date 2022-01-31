using System;
using System.Collections.Generic;

namespace TodoApp.DAL.Entities
{
    public sealed class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Sex { get; set; }
        public List<User> Friends { get; set; }
        public List<Project> Projects { get; set; }
    }
}