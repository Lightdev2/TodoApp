using System.Collections;

namespace TodoApp.Core.Services
{
    public interface ITodoService
    {
        public IEnumerable GetTodos();
        
    }
}