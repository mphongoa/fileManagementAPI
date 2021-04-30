using System;
using System.Collections.Generic;
using System.Text;

namespace Task.Domain.Users.Commands
{
    public class AuthCommand
    {
        public string EmailAddress { get; set; }

        public string Password { get; set; }
    }
}
