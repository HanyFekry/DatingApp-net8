using AutoMapper;
using DatingApi.Data;
using DatingApi.Dtos;
using DatingApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace DatingApi.Repositories
{
    public class UserRepository(ApplicationDbContext context, IMapper mapper) : Repository<AppUser>(context), IUserRepository
    {
        public async Task<AppUser?> GetByUserName(string userName)
        {
            return await _dbSet.Include(x => x.Photos).FirstOrDefaultAsync(x => x.UserName == userName);
        }

        public async Task<MemberDto?> GetMemberByUserName(string userName)
        {
            //return await context.Users
            //    .ProjectTo<MemberDto>(mapper.ConfigurationProvider)
            //    .FirstOrDefaultAsync(x => x.UserName == userName);
            var user = await context.Users.Include(x => x.Photos).FirstOrDefaultAsync(x => x.UserName == userName);
            return mapper.Map<MemberDto?>(user);
        }
    }
}
