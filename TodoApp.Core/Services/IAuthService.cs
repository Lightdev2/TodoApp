using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Core.DTOs;

namespace TodoApp.Core.Services
{
    public interface IAuthService
    {
        Task<bool> VerifyUser(UserCredentials credentials);
    }
}
