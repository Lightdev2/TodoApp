using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.DAL.Entities;

namespace TodoApp.Core.Repositories
{
    public interface IUserSessionsRepository
    {
        Task<int> Add(UserSession session);
        Task<UserSession> FindSessionByToken(string token);
    }
}
