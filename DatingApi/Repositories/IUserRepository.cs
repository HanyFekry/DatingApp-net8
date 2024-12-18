using DatingApi.Dtos;
using DatingApi.Entities;

namespace DatingApi.Repositories
{
    public interface IUserRepository : IRepository<AppUser>
    {
        Task<AppUser?> GetByUserName(string userName);
        Task<MemberDto?> GetMemberByUserName(string userName);
    }
}
