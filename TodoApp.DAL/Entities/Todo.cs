using System;
using System.ComponentModel.DataAnnotations;

namespace TodoApp.DAL.Entities
{
    public sealed class Todo
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public DateTime Deadline { get; set; }
        public bool IsFinished { get; set; }
    }
}