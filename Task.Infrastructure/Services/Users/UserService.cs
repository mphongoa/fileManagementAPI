using System.Collections.Generic;
using System.Linq;
using Task.Domain.BaseTypes;
using Task.Domain.Users;
using Task.Domain.Users.Commands;
using Task.Infrastructure.Database;
using Task.Infrastructure.Services.Auth;

namespace Task.Infrastructure.Services.Users
{
    public class UserService : IUserService
    {
        private readonly DataContext _taskContext;
        private readonly IAuthService _authService;

        public UserService(DataContext taskContext, IAuthService authService )
        {
            _taskContext = taskContext;
            _authService = authService;
        }

        public List<User> GetUsers()
        {
            return _taskContext.Users.OrderBy(user => user.Name).ToList();
        }

        public User AddUser(UserCommand addCommand)
        {
            string defaultpassword = "12345";

            var user = new User()
            {
                Name = addCommand.Name,
                EmailAddress = addCommand.EmailAddress
            };

            _authService.Register(user, defaultpassword);

            return user;
        }
        
        public void UpdateUser(UserCommand updateCommand)
        {
            if (_taskContext.Users.Any(usr => usr.EmailAddress == updateCommand.EmailAddress && usr.UserId != updateCommand.UserId))
            {
                throw new TaskException("User with specified email already exists.");
            }

            User user = _taskContext.Users.Find(updateCommand.UserId);

            
            user.Name = updateCommand.Name;
            
            user.EmailAddress = updateCommand.EmailAddress;
            

          //  user.SetEntityUpdateDetails(updateCommand.UserId);

          //  _taskContext.Save();
        }

        public User GetUserByEmail(string email)
        {
            return _taskContext.Users.FirstOrDefault(user => user.EmailAddress == email);
        }

        public bool UserExists(string email)
        {
            return _taskContext.Users.Any(user => user.EmailAddress == email);
        }

        public User GetUserById(int userId)
        {
            return _taskContext.Users.Find(userId);
        }

        public void DeleteUser(int userId)
        {
            var user = _taskContext.Users.Find(userId);

            _taskContext.Remove(user);

            _taskContext.SaveChanges();
        }
    }
}
