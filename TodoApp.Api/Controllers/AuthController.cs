using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using TodoApp.Core.Infrastructure;
using TodoApp.Core.DTOs;
using TodoApp.Core.Services;

namespace TodoApp.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
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

            return Ok(jwt);
        }
    }
}
