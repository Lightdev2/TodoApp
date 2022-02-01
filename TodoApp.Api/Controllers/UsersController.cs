using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoApp.DAL.Entities;
using TodoApp.Core.Services;

namespace TodoApp.Api.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

    }
}
