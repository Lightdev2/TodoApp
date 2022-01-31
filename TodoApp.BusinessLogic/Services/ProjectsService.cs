using System.Threading.Tasks;
using TodoApp.Core.Services;
using TodoApp.Core.Repositories;
using TodoApp.DAL.Entities;

namespace TodoApp.BusinessLogic.Services
{
    public class ProjectsService : IProjectsService
    {
        private readonly IProjectsRepository _projectsRepository;

        public ProjectsService(IProjectsRepository projectsRepository)
        {
            _projectsRepository = projectsRepository;
        }
        public async Task<int> CreateProject(Project project)
        {
            var result = await _projectsRepository.Add(project);
            return result;
        }
    }
}
