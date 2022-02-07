using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Core.Services;
using TodoApp.DAL.Entities;

namespace TodoApp.Api.Controllers
{
    [ApiController]
    [Route("api/projects")]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectsService _projectsService;

        public ProjectsController(IProjectsService projectsService)
        {
            _projectsService = projectsService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateProject(Project project)
        {
            if (string.IsNullOrEmpty(project.Title)) return BadRequest();
            var user = (User)HttpContext.Items["User"];
            var result = await _projectsService.CreateProjectAsync(project, user);
            return Ok(result.ToString());
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateProject(Project project)
        {
            var user = (User)HttpContext.Items["User"];
            if (string.IsNullOrEmpty(project.Title)) return BadRequest();
            var result = await _projectsService.UpdateProjectAsync(project);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProject(Project project)
        {
            return Ok();
        }

        [HttpPost]
        [Authorize]
        [Route("all")]
        public async Task<IActionResult> GetUserProjects()
        {
            var user = (User)HttpContext.Items["User"];
            var result = await _projectsService.GetUserProjectsAsync(user);
            return Ok(result);
        }

    }
}
