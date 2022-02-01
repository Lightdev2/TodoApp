using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using TodoApp.Core.Infrastructure;
using TodoApp.Core.DTOs;
using TodoApp.Core.Repositories;
using TodoApp.Core.Services;

namespace TodoApp.BusinessLogic.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<bool> VerifyUser(UserCredentials credentials)
        {
            var result = await _userRepository.FindByEmail(credentials.Email);
            if (result != null)
            {
                return result.Password == credentials.Password;
            }

            return false;
        }

        public string GenerateJwtToken(UserCredentials userCredentials, List<Claim> claims)
        {
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.Issuer,
                audience: AuthOptions.Audience,
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(5)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
