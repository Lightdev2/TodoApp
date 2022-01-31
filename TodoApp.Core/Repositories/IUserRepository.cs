using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Core.DTOs;

namespace TodoApp.Core.Repositories
{
    public interface IUserRepository
    {
        Task<int> Add(User user);
        Task<bool> Delete(User user);
        Task<int> Update(User user);
        Task<User> FindById(int id);
    }
}
