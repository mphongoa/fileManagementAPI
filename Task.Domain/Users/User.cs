using System;
using System.Collections.Generic;
using System.Text;
using Task.Domain.BaseTypes;

namespace Task.Domain.Users
{
    public class User 
    {
        public int UserId { get; set; } 
        public string Name { get; set; }
        public string EmailAddress { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

    }
}
