using System.Threading.Tasks;
using TodoApp.DAL.Entities;

namespace TodoApp.Core.Repositories
{
    public interface ITodoRepository
    {
        Task<int?> Add(Todo todo);

    }
}