using Microsoft.AspNetCore.Mvc;
using TodoApp.Core.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using TodoApp.BusinessLogic.Bus;
using TodoApp.Core;
using TodoApp.Core.DTOs;
using TodoApp.Core.Repositories;
using EvtBus = TodoApp.BusinessLogic.Bus.EvtBus;
using Todo = TodoApp.DAL.Entities.Todo;
using User = TodoApp.DAL.Entities.User;

namespace TodoApp.Api.Controllers
{
    [ApiController]
    [Route("api/todos")]
    public class TodoController : ControllerBase
    {
        private readonly EvtBus _evtBus;
        private readonly ITodoService _todoService;
        private readonly IProjectsRepository _projectsRepository;

        public TodoController(ITodoService service, EvtBus evtBus, IProjectsRepository projectsRepository)
        {
            _todoService = service;
            _evtBus = evtBus;
            _projectsRepository = projectsRepository;
        }

        [HttpPost]
        [Route("all")]
        [Authorize]
        public async Task<IActionResult> GetTodosInProject(ProjectId projectId)
        {
            if (projectId.end - projectId.start > 100) return BadRequest();
            var result = await _todoService.GetAllTodosInProjectAsync(projectId);
            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateTodo(newTodo todo)
        {
            var user = (User)HttpContext.Items["User"];
            var project = await _projectsRepository.FindProjectById(todo.ProjectId);
            var todoToSave = new Todo
            {
                Title = todo.Title,
                Desc = todo.Desc,
                Project = project
            };
            var result = await _todoService.CreateTodo(todoToSave, user);
            _evtBus.NotifyObservers(new Message
            {
                email = user.Email,
                msg = " added new todo into project " + project.Title + " by ",
            });
            return Ok(result);
        }

        [HttpGet]
        [Route("delete")]
        public async Task<IActionResult> DeleteTodo()
        {
            var msg = new Message();
            msg.email = "stas12102001@gmail.com";
            msg.msg = "delete todo";
            _evtBus.NotifyObservers(msg);
            return Ok();
        }

        [HttpPost]
        [Authorize]
        [Route("edit")]
        public async Task<IActionResult> UpdateTodo(TodoToUpdate todo)
        {
            var result = await _todoService.UpdateTodoAsync(todo);
            return Ok(result);
        }

    }

    public class newTodo
    {
        public int ProjectId { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
    }

}