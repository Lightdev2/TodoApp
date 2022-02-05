using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using TodoApp.Api.Contracts;
using TodoApp.Core.Infrastructure;
using TodoApp.Core.DTOs;
using TodoApp.Core.Repositories;
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
        private readonly IUserSessionService _sessionService;
        private readonly IUserRepository _userRepository;
        public AuthController(IUserRepository userRepository,IAuthService authService, IUserService userService, IUserSessionService sessionService)
        {
            _authService = authService;
            _userRepository = userRepository;
            _userService = userService;
            _sessionService = sessionService;
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
            var refreshToken = Guid.NewGuid().ToString();
            var user = await _userRepository.FindByEmail(userCredentials.Email);
            var result = await _sessionService.CreateSession(user);
            var response = new LoginResponse
            {
                Email = userCredentials.Email,
                Token = jwt,
                RefreshToken = result,
            };
            HttpContext.Response.Cookies.Append("Token", "asd");
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

        [HttpPost]
        [Route("refresh")]
        public async Task<IActionResult> RefreshToken(RefreshRequest req)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(req.Token, new TokenValidationParameters
                {
                    ValidIssuer = AuthOptions.Issuer,
                    ValidAudience = AuthOptions.Audience,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                    ValidateIssuerSigningKey = true,
                }, out SecurityToken token);
                var result = await _sessionService.FindSessionByToken(req.RefreshToken);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
