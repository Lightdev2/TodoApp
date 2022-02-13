using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.Core.DTOs
{
    public class TodoToUpdate
    {
        public int id { get; set; }
        public int projectId { get; set; }
        public string desc { get; set; }
        public string title { get; set; }
        public bool isFinished { get; set; }
    }
}
