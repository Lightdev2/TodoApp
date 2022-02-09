using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApp.Core.DTOs;
using Todo = TodoApp.DAL.Entities.Todo;

namespace TodoApp.Core.Repositories
{
    public interface ITodoRepository
    {
        Task<int?> Add(Todo todo);
        Task<List<Todo>> GetAllInProject(ProjectId projectId);
    }
}