using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Task.Domain.Users;

namespace Task.Infrastructure.Services.Auth
{
    public interface IAuthService
    {
        Task<User> Register(User user, string password);
        Task<User> Login(string emailAddress, string password);
        Task<bool> UserExist(string emailAddress);
    }
}
