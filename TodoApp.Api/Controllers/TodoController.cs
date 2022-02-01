using Microsoft.AspNetCore.Mvc;
using TodoApp.Core.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using TodoApp.Core;
using TodoApp.DAL.Entities;

namespace TodoApp.Api.Controllers
{
    [ApiController]
    [Route("api/todos")]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;

        public TodoController(ITodoService service)
        {
            _todoService = service;
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
            var result = await _todoService.CreateTodo(todo);
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