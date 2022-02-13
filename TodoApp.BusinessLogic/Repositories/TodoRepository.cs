using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoApp.Core.DTOs;
using TodoApp.Core.Repositories;
using TodoApp.DAL;
using Todo = TodoApp.DAL.Entities.Todo;

namespace TodoApp.BusinessLogic.Repositories
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

        public async Task<List<Todo>> GetAllInProject(ProjectId projectId)
        {
            var result = await _context.Todos
                .Where(x => x.ProjectId == projectId.id)
                .Skip(projectId.start)
                .Take(projectId.end)
                .ToListAsync();

            return result;
        }

        public async Task<bool> UpdateTodo(Todo todo)
        {
            var oldTodo = await _context.Todos.FirstOrDefaultAsync(x => x.Id == todo.Id);
            oldTodo.Desc = todo.Desc;
            oldTodo.IsFinished = todo.IsFinished;
            oldTodo.Title = todo.Title;
            oldTodo.LastModifiedDate = DateTime.UtcNow;
            _ = await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Todo> FindTodoById(int id)
        {
            return new Todo();
        }
    }
}