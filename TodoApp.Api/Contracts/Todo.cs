using System;

namespace TodoApp.Api.Contracts
{
    public class Todo
    {
        public int Id { get; set; }
        public string Desc { get; set; }
        public string Title { get; set; }
        public DateTime Deadline { get; set; }
        public bool IsFinished { get; set; }
    }
}