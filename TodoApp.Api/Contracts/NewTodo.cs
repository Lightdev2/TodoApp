using System;

namespace TodoApp.Api.Contracts
{
    public class NewTodo
    {
        public string Desc { get; set; }
        public string Title { get; set; }
        public DateTime Deadline { get; set; }
    }
}