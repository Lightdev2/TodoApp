using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.DAL.Entities;

namespace TodoApp.Core.Services
{
    public interface IUserService
    {
        Task<int> CreateUser(User user);
    }
}
