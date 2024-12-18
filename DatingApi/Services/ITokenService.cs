using DatingApi.Entities;

namespace DatingApi.Services
{
    public interface ITokenService
    {
        Task<string> CreateToken(AppUser user);
    }
}
