using DatingApi.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DatingApi.Services
{
    public class TokenService(IConfiguration config) : ITokenService
    {
        public Task<string> CreateToken(AppUser user)
        {
            string key = config.GetValue<string>("Jwt:TokenKey") ?? throw new Exception("Token key not found.");
            if (key.Length < 64) throw new Exception("Token key can't be less than 64 char.");
            var sKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var cred = new SigningCredentials(sKey, SecurityAlgorithms.HmacSha512Signature);
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier,user.UserName)
            };
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7), // Token expiration time
                Issuer = config.GetValue<string>("Jwt:Issuer"),
                SigningCredentials = cred
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Task.FromResult(tokenHandler.WriteToken(token));
        }
    }
}
