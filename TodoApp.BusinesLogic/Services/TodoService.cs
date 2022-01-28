using System.Collections;
using System.Collections.Generic;
using TodoApp.Core.Services;

namespace TodoApp.BusinesLogic.Services
{
    public class TodoService : ITodoService
    {
        public IEnumerable GetTodos()
        {
            var list = new List<string>();
            for (int i = 0; i < 15; i++)
            {
                list.Add("Todo" + i.ToString());
            }
            return list;
        }
    }
}