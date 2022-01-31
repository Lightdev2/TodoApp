using System.Collections.Generic;

namespace TodoApp.DAL.Entities
{
    public sealed class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Project> Projects { get; set; }
    }
}