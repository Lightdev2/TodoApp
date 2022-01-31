using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.DAL.Entities;
using TodoApp.Core.Repositories;
using TodoApp.Core.Services;

namespace TodoApp.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<int> CreateUser(User user)
        {
            var result = await _userRepository.Add(user);
            return result;
        }
    }
}
