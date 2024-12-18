using AutoMapper;
using DatingApi.Dtos;
using DatingApi.Entities;
using DatingApi.Extensions;
using DatingApi.Repositories;
using DatingApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Security.Claims;

namespace DatingApi.Controllers
{
    public class UsersController(IUserRepository repository, IMapper mapper, IStringLocalizer<UsersController> localizer
        , IPhotoService photoService) : ApiControllerBase
    {
        [Authorize]
        [HttpGet]
        [ProducesDefaultResponseType]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
            var users = await repository.GetAllIncludesAsync(includeProperties: x => x.Photos);
            var dto = mapper.Map<IEnumerable<MemberDto>>(users);
            return Ok(dto);
        }
        [Authorize]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<MemberDto>> GetUser(int id)
        {
            var user = await repository.Get(id);
            var dto = mapper.Map<MemberDto>(user);
            return Ok(dto);
        }
        [Authorize]
        [HttpGet("{userName}")]
        public async Task<ActionResult<MemberDto>> GetUser(string userName)
        {
            var user = await repository.GetMemberByUserName(userName);
            if (user == null) return NotFound();
            //var dto = mapper.Map<MemberDto>(user);
            //return Ok(dto);
            return Ok(user);
        }
        [Authorize]
        [HttpPut]
        public async Task<ActionResult> UpdateUser(int id, MemberUpdateDto member)
        {
            var userName = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userName)) return BadRequest("No user name found in token");
            var user = await repository.GetByUserName(userName);
            if (user == null) return BadRequest("user not found");
            mapper.Map(member, user);
            await repository.Update(user);
            return NoContent();
        }
        [Authorize]
        [HttpPost("add-photo")]
        public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file)
        {
            var user = await repository.GetByUserName(User.GetUserName());
            if (user == null) return BadRequest("can not find user");
            var result = await photoService.AddPhotoAsync(file);
            if (result.Error != null) return BadRequest(result.Error.Message);
            var photo = new Photo
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId
            };
            user.Photos.Add(photo);
            var photosResult = await repository.SaveChangesAsync();
            if (photosResult > 0)
                return CreatedAtAction(nameof(GetUser), new { userName = user.EnName }, mapper.Map<PhotoDto>(photo));
            return BadRequest("error adding photo to database.!");
        }

        [HttpGet("test-localization")]
        public async Task<ActionResult<string>> TestLocalization()
        {
            return Ok(localizer["welcome"].Value);
        }
    }
}
