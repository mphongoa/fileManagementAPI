using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task.Domain.Users;
using Task.Domain.Users.Commands;
using Task.Infrastructure.Services.Auth;
using Task.Infrastructure.Services.Users;

namespace Task.Application.Controllers
{
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _config;
        public UserController(IUserService userService, IConfiguration config)
        {
            _userService = userService;
            _config = config;
        }

        [HttpGet("users")]
        public  List<User> GetUsers()
        {
            return _userService.GetUsers();
        }

        [HttpGet("user")]
        public User GetUser(string  email)
        {
            return _userService.GetUserByEmail(email);
        }

        [HttpPost("add")]
        public User AddUser(UserCommand addCommand)
        {
            return _userService.AddUser(addCommand);
        }

        [HttpPut("edit")]
        public void UpdateUser(UserCommand updateCommand)
        {
             _userService.UpdateUser(updateCommand);  
        }

        [HttpDelete("user")]
        public void DeleteUser(int id)
        {
            _userService.DeleteUser(id);
        }
    }
}