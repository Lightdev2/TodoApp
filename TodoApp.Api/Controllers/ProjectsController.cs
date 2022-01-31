using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Core.Services;
using TodoApp.DAL.Entities;

namespace TodoApp.Api.Controllers
{
    [ApiController]
    [Route("api/projects")]
    public class ProjectsController : ControllerBase
    {
        public readonly IProjectsService _projectsService;

        public ProjectsController(IProjectsService projectsService)
        {
            _projectsService = projectsService;
        }

        [HttpPost]
        public async Task<int> CreateProject(Project project)
        {
            var result = await _projectsService.CreateProject(project);
            return result;
        }
    }
}
