using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoApp.DAL.Entities;
using TodoApp.Core.Repositories;
using TodoApp.DAL;

namespace TodoApp.BusinessLogic.Repositories
{
    public class UserSessionsRepository : IUserSessionsRepository
    {
        private readonly TodoAppContext _context;

        public UserSessionsRepository(TodoAppContext context)
        {
            _context = context;
        }
        public async Task<int> Add(UserSession session)
        {
            var result = await _context.Sessions.AddAsync(session);
            _ = await _context.SaveChangesAsync();
            return result.Entity.Id;
        }

        public async Task<UserSession> FindSessionByToken(string token)
        {
            var result = await _context.Sessions.FirstOrDefaultAsync(x => x.RefreshToken == token);
            return result ?? null;
        }
    }
}
