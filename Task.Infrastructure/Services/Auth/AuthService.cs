using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Domain.BaseTypes;
using Task.Domain.Users;
using Task.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Task.Infrastructure.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly DataContext _taskContext;
        public AuthService(DataContext taskContext)
        {
            _taskContext = taskContext;
        }
        public  async Task<User> Login(string emailAddress, string password)
        {
            var user = await _taskContext.Users.FirstOrDefaultAsync(x => x.EmailAddress == emailAddress);

            if(user == null)
            {
                return null;
            }

            if(!VerifyPasswordHash(password,user.PasswordHash, user.PasswordSalt))
            {
                return null;
            }

            return user;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
        
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for(int i=0; i<computedHash.Length; i++)
                {
                    if(computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }  
                }
            }

            return true;
        }

        public async Task<User> Register(User user, string password)
        {
            if (_taskContext.Users.Any(usr => usr.EmailAddress == user.EmailAddress))
            {
                throw new TaskException("User with specified email already exists");
            }
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _taskContext.Users.Add(user);

            _taskContext.SaveChanges();

            return await System.Threading.Tasks.Task.FromResult(user);

        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<bool> UserExist(string emailAddress)
        {
            if(await _taskContext.Users.AnyAsync(x => x.EmailAddress == emailAddress))
            {
                return true;
            }
            return false;
        }
    }
}
