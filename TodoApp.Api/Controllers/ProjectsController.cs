using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoApp.BusinessLogic.Bus;
using TodoApp.Core.DTOs;
using TodoApp.Core.Services;
using TodoApp.DAL.Entities;
using User = TodoApp.DAL.Entities.User;

namespace TodoApp.Api.Controllers
{
    [ApiController]
    [Route("api/projects")]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectsService _projectsService;
        private readonly EvtBus _eventBus;

        public ProjectsController(IProjectsService projectsService, EvtBus eventBus)
        {
            _projectsService = projectsService;
            _eventBus = eventBus;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateProject(Project project)
        {
            if (string.IsNullOrEmpty(project.Title)) return BadRequest();
            var user = (User)HttpContext.Items["User"];
            var result = await _projectsService.CreateProjectAsync(project, user);
            _eventBus.NotifyObservers(new Message
            {
                email = user.Email,
                msg = "created project"
            });
            return Ok(result.ToString());
        }

        [HttpPatch]
        [Authorize]
        public async Task<IActionResult> UpdateProject(Project project)
        {
            var user = (User)HttpContext.Items["User"];
            if (string.IsNullOrEmpty(project.Title)) return BadRequest();
            var result = await _projectsService.UpdateProjectAsync(project, user);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteProject(ProjectId id)
        {
            var user = (User)HttpContext.Items["User"];
            var result = await _projectsService.DeleteProjectAsync(id.id, user);
            _eventBus.NotifyObservers(new Message
            {
                email = user.Email,
                msg = "deleted project"
            });
            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        [Route("all")]
        public async Task<IActionResult> GetUserProjects()
        {
            var user = (User) HttpContext.Items["User"];
            var result = await _projectsService.GetUserProjectsAsync(user);
            return Ok(result);
        }

    }
}
