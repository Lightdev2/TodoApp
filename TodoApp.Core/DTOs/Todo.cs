using System;

namespace TodoApp.Core.DTOs
{
    public class Todo
    {
        public int Id { get; set; }
        public string Desc { get; set; }
        public string Title { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsFinished { get; set; }
    }
}