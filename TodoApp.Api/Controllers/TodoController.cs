using Microsoft.AspNetCore.Mvc;
using TodoApp.Core.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using TodoApp.BusinessLogic.Bus;
using TodoApp.Core;
using TodoApp.DAL.Entities;
using EvtBus = TodoApp.BusinessLogic.Bus.EvtBus;

namespace TodoApp.Api.Controllers
{
    [ApiController]
    [Route("api/todos")]
    public class TodoController : ControllerBase
    {
        private readonly EvtBus _evtBus;
        private readonly ITodoService _todoService;

        public TodoController(ITodoService service, EvtBus evtBus)
        {
            _todoService = service;
            _evtBus = evtBus;
        }

        [HttpGet]
        public async Task<IActionResult> GetTodos()
        {
            return Ok();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateTodo(Todo todo)
        {
            var user = (User)HttpContext.Items["User"];
            var result = await _todoService.CreateTodo(todo, user);
            _evtBus.NotifyObservers(new Message
            {
                email = user.Email,
                msg = "new data available",
            });
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTodo()
        {
            return Ok();
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateTodo()
        {
            return Ok();
        }
    }
}