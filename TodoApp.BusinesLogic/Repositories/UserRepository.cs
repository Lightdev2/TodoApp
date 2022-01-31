using System;
using System.Threading.Tasks;
using TodoApp.Core.DTOs;
using TodoApp.Core.Repositories;
using TodoApp.DAL;

namespace TodoApp.BusinesLogic.Repositories
{
    internal class UserRepository : IUserRepository
    {
        private readonly TodoAppContext _context;
        public UserRepository(TodoAppContext context)
        {
            _context = context;
        }

        public async Task<int> Add(User user)
        {
            throw new NotImplementedException();
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
