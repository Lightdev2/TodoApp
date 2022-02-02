using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using TodoApp.Core.Infrastructure;
using System.Security.Claims;
using TodoApp.Core.Repositories;

namespace TodoApp.Api.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IUserRepository userRepository)
        {
            var jwtToken = context.Request.Headers["Authorization"][0].Split(" ").Last();
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(jwtToken, new TokenValidationParameters
                {
                    ValidIssuer = AuthOptions.Issuer,
                    ValidAudience = AuthOptions.Audience,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                    ValidateIssuerSigningKey = true,
                }, out SecurityToken token);
                var claims = (JwtSecurityToken)token;
                string email = claims.Claims.First(x => x.Type == ClaimTypes.Email).Value;
                var result = await userRepository.FindByEmail(email);
                context.Items["User"] = result;
                await _next.Invoke(context);
            }
            catch
            {
            }
            await _next.Invoke(context);
        }

    }
}
