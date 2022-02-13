using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApp.Core.DTOs;
using Todo = TodoApp.DAL.Entities.Todo;
using User = TodoApp.DAL.Entities.User;

namespace TodoApp.Core.Services
{
    public interface ITodoService
    {
        Task<int?> CreateTodo(Todo newTodo, User user);
        Task<List<Todo>> GetAllTodosInProjectAsync(ProjectId id);
        Task<bool> UpdateTodoAsync(TodoToUpdate todo);
    }
}