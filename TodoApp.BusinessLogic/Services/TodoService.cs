using System;
using System.Threading.Tasks;
using TodoApp.Core.Services;
using TodoApp.Core;
using TodoApp.Core.Repositories;
using TodoApp.DAL.Entities;

namespace TodoApp.BusinessLogic.Services
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _todoRepository;
        
        public TodoService(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }
        
        public async Task<int?> CreateTodo(Todo todo)
        {
            todo.CreatedAt = DateTime.Now;
            var result = await _todoRepository.Add(todo);
            
            return result;
        }
    }
}