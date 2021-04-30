using System;
using System.Collections.Generic;
using System.Text;

namespace Task.Domain.Users.Commands
{
    public class UserCommand
    {
        public UserCommand(string name, string emailAddress, string password)
        {
            
            Name = name;
           
            EmailAddress = emailAddress;

            Password = password;


        }

        public int UserId { get; set; }
         
        public string Name { get; }
        
        public string EmailAddress { get; set; }

        public string Password { get; set; }

    }
}
