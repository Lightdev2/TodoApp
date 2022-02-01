#nullable enable
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoApp.DAL.Entities;
using TodoApp.Core.Repositories;
using TodoApp.DAL;

namespace TodoApp.BusinessLogic.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TodoAppContext _context;
        public UserRepository(TodoAppContext context)
        {
            _context = context;
        }

        public async Task<int> Add(User user)
        {
            var result = await _context.AddAsync(user);
            _ = _context.SaveChangesAsync();
            return result.Entity.Id;
        }

        public async Task<User?> FindByEmail(string email)
        {
            var result = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
            return result;
        }

        public async Task<int> Update(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Delete(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<User> FindById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
