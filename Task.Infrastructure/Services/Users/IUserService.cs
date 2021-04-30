using System;
using System.Collections.Generic;
using System.Text;
using Task.Domain.Users;
using Task.Domain.Users.Commands;

namespace Task.Infrastructure.Services.Users
{
    public interface IUserService
    {
        List<User> GetUsers();
        User AddUser(UserCommand addCommand);
        void UpdateUser(UserCommand updateCommand);
        User GetUserByEmail(string email);
        bool UserExists(string email);
        User GetUserById(int userId);
        void DeleteUser(int userId);
    }
}
