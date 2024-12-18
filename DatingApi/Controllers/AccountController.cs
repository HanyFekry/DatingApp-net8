using DatingApi.Data;
using DatingApi.Dtos;
using DatingApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace DatingApi.Controllers
{
    public class AccountController(ApplicationDbContext context, ITokenService tokenService) : ApiControllerBase
    {
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto dto)
        {
            return Ok();
            //if (await UserExists(dto.UserName)) return BadRequest("user name is already taken.");
            //var hmac = new HMACSHA512();
            //AppUser user = new AppUser
            //{
            //    UserName = dto.UserName,
            //    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password)),
            //    PasswordSalt = hmac.Key
            //};

            //await context.Users.AddAsync(user);
            //await context.SaveChangesAsync();
            //return Ok(new UserDto
            //{
            //    UserName = user.UserName,
            //    Token = await tokenService.CreateToken(user)
            //});
        }
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto dto)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.UserName.ToLower() == dto.UserName.ToLower());
            if (user == null) return Unauthorized("Invalid user name.");
            var hmac = new HMACSHA512(user.PasswordSalt);
            var passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password));
            for (int i = 0; i < passwordHash.Length; i++)
            {
                if (passwordHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid password.");
            }
            return Ok(new UserDto
            {
                UserName = user.UserName,
                Token = await tokenService.CreateToken(user)
            });
        }
        private async Task<bool> UserExists(string userName)
        {
            return await context.Users.AnyAsync(x => x.UserName.ToLower() == userName.ToLower());
        }
    }
}
