using System;
using System.Threading.Tasks;
using TodoApp.BusinessLogic.Bus;
using TodoApp.Core.Services;
using TodoApp.Core;
using TodoApp.Core.Repositories;
using TodoApp.DAL.Entities;


namespace TodoApp.BusinessLogic.Services
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _todoRepository;

        public TodoService(ITodoRepository todoRepository, EvtBus evtBus)
        {
            _todoRepository = todoRepository;
        }
        public async Task<int?> CreateTodo(Todo todo, User user)
        {
            todo.CreatedAt = DateTime.Now;
            todo.Creator = user;
            var result = await _todoRepository.Add(todo);
            return result;
        }
    }
}