using System;
using System.Threading.Tasks;
using TodoApp.Core.Repositories;
using TodoApp.DAL.Entities;
using TodoApp.DAL;

namespace TodoApp.BusinesLogic.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly TodoAppContext _context;

        public TodoRepository(TodoAppContext context)
        {
            _context = context;
        }
        public async Task<int?> Add(Todo todo)
        {
            var result = await _context.Todos.AddAsync(todo);
            if(result is null)
                return null;
            _ = await _context.SaveChangesAsync();
            return result.Entity.Id;
        }
    }
}