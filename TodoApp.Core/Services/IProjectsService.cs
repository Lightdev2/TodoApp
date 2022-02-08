using System.Collections.Generic;
using TodoApp.DAL.Entities;
using System.Threading.Tasks;

namespace TodoApp.Core.Services
{
    public interface IProjectsService
    {
        Task<int> CreateProjectAsync(Project project, User user);
        Task<int?> UpdateProjectAsync(Project project, User user);
        Task<bool> DeleteProjectAsync(int projectId, User user);
        Task<List<Project>> GetUserProjectsAsync(User user);
    }
}
