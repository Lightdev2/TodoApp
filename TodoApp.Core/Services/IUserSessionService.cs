using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.DAL.Entities;

namespace TodoApp.Core.Services
{
    public interface IUserSessionService
    {
        Task<string> CreateSession(User user);
        Task<UserSession> FindSessionByToken(string token);
    }
}
