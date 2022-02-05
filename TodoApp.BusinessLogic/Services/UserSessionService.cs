using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Core.Repositories;
using TodoApp.DAL.Entities;
using TodoApp.Core.Services;

namespace TodoApp.BusinessLogic.Services
{
    public class UserSessionService : IUserSessionService
    {
        private readonly IUserSessionsRepository _userSessionsRepository;
        public UserSessionService(IUserSessionsRepository userSessionsRepository)
        {
            _userSessionsRepository = userSessionsRepository;
        }
        public async Task<string> CreateSession(User user)
        {
            var token = Guid.NewGuid().ToString();
            var createdAt = DateTime.UtcNow;
            var expiersIn = DateTime.UtcNow.AddDays(28);
            var userSessions = new UserSession
            {
                RefreshToken = token,
                CreatedAt = createdAt,
                ExpiresIn = expiersIn,
                User = user,
            };
            var result = await _userSessionsRepository.Add(userSessions);
            return token;
        }

        public async Task<UserSession> FindSessionByToken(string token)
        {
            var result = await _userSessionsRepository.FindSessionByToken(token);
            return result;
        }
    }
}
