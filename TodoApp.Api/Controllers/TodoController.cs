using Microsoft.AspNetCore.Mvc;
using TodoApp.Core.Services;
using System.Threading.Tasks;

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
        public async Task<IActionResult> CreateTodo()
        {
            return Ok();
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