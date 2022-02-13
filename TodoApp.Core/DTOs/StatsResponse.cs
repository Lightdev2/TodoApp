using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.Core.DTOs
{
    public class StatsResponse
    {
        public Dictionary<string,int> Stats { get; set; }
    }
}
