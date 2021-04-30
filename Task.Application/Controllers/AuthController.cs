using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Task.Domain.Users;
using Task.Domain.Users.Commands;
using Task.Infrastructure.Services.Auth;

namespace Task.Application.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IConfiguration _config;
        public AuthController(IAuthService authService, IConfiguration config)
        {
            _authService = authService;
            _config = config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserCommand userDto)
        {
            //validate
            userDto.EmailAddress = userDto.EmailAddress.ToLower();

           /* if(await _authService.UserExist(userDto.EmailAddress)) {
                return BadRequest("user already exist");
            }*/

            var userToCreate = new User
            {
                EmailAddress = userDto.EmailAddress,
                Name = userDto.Name
            };

            var createdUser = await _authService.Register(userToCreate, userDto.Password);

            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login (AuthCommand auth)
        {
            //validate.
            var user = await _authService.Login(auth.EmailAddress, auth.Password);

            if (user == null)
            {
                return Unauthorized();
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.EmailAddress),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new
            {
                token = tokenHandler.WriteToken(token)
            });
        }
    }
}