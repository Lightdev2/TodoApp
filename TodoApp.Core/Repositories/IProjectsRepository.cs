using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.DAL.Entities;

namespace TodoApp.Core.Repositories
{
    public interface IProjectsRepository
    {
        Task<int>AddProjectAsync(Project project);
        Task<int?>UpdateProjectAsync(Project project, User user);
        Task<bool> DeleteProjectAsync(Project project);
        Task<Project> FindProjectById(int projectId);
        Task<List<Project>> GetUserProjectsAsync(User user);
    }
}
