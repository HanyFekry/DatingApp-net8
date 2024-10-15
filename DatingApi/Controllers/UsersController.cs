using DatingApi.Data;
using DatingApi.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingApi.Controllers
{
    [ApiController]
    public class UsersController(ApplicationDbContext context) : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            var users = await context.Users.ToListAsync();
            return Ok(users);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUser(int id)
        {
            var user = await context.Users.FindAsync(id);
            return Ok(user);
        }
    }
}
