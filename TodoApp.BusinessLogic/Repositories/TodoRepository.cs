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
    }
}