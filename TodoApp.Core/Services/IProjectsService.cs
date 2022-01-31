using TodoApp.DAL.Entities;
using System.Threading.Tasks;

namespace TodoApp.Core.Services
{
    public interface IProjectsService
    {
        Task<int> CreateProject(Project project);
    }
}
