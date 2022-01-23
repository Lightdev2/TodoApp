using Microsoft.AspNetCore.Mvc;
using TodoApp.Core.Services;

namespace TodoApp.Api.Controllers
{
    [ApiController]
    [Route("api/todo")]
    public class TodoController : ControllerBase
    {
        private ITodoService _todoService;
        public TodoController(ITodoService service)
        {
            _todoService = service;
        }
        [Route("index")]
        public IActionResult Index()
        {
            var result = _todoService.GetTodos();
            return Ok(result);
        }
    }
}