using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
