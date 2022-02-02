using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using TodoApp.Api.Contracts;
using TodoApp.Core.Infrastructure;
using TodoApp.Core.DTOs;
using TodoApp.DAL.Entities;
using TodoApp.Core.Services;

namespace TodoApp.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        public AuthController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(UserCredentials userCredentials)
        {
            if (string.IsNullOrEmpty(userCredentials.Email) || string.IsNullOrEmpty(userCredentials.Password))
            {
                return BadRequest();
            }

            var isValidUser = await _authService.VerifyUser(userCredentials);
            if (!isValidUser) return Unauthorized();
            var claims = new List<Claim> { new Claim(ClaimTypes.Email, userCredentials.Email) };
            var jwt = _authService.GenerateJwtToken(userCredentials, claims);
            var response = new LoginResponse
            {
                Email = userCredentials.Email,
                Token = jwt,
            };
            var token = HttpContext.Items["Token"];
            return Ok(response);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(NewUser newUser)
        {
            if (string.IsNullOrEmpty(newUser.Password) || string.IsNullOrEmpty(newUser.Email))
            {
                return BadRequest();
            }
            var user = new DAL.Entities.User
            {
                Email = newUser.Email,
                Password = newUser.Password,
                CreatedAt = DateTime.UtcNow,
                Sex = newUser.Sex,
            };
            var result = await _userService.CreateUser(user);
            return Ok(result);
        }
    }
}
