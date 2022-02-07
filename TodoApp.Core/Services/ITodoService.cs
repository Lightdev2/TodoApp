using System.Collections;
using System.Threading.Tasks;
using TodoApp.DAL.Entities;
namespace TodoApp.Core.Services
{
    public interface ITodoService
    {
        Task<int?> CreateTodo(Todo newTodo, User user);
    }
}