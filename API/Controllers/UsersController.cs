using System.Linq;
using System.Security.Claims;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using API.Extensions;
using API.Entities;

namespace API.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly iUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly iPhotoService _photoService;
        public UsersController(iUserRepository userRepository, IMapper mapper, iPhotoService photoService)
        {
            _photoService = photoService;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        //[Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> getUsers()
        {
            // var users = await _userRepository.GetUsersAsync();
            return Ok(await _userRepository.GetMembersAsync());
        }

        //[AllowAnonymous]
        [HttpGet("{username}", Name = "GetUser")]
        public async Task<ActionResult<MemberDto>> getUser(string username)
        {
            // var user = await _userRepository.GetUserByUsernameAsync(username);
            return await _userRepository.GetMemberAsync(username);
        }

        [HttpPut]
        public async Task<ActionResult> updateUser(MemberUpdateDto member)
        {
            //var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var username = User.GetUserName();
            var user = await _userRepository.GetUserByUsernameAsync(username);

            _mapper.Map(member, user);
            _userRepository.Update(user);

            if (await _userRepository.SaveAllAsync()) return NoContent();

            return BadRequest("Failed to update user");

        }

        [HttpPost("add-photo")]
        public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file)
        {
            var user = await _userRepository.GetUserByUsernameAsync(User.GetUserName());
            var result = await _photoService.AddPhotoAsync(file);

            if (result.Error != null) return BadRequest(result.Error.Message);

            var photo = new Photo
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId
            };

            if (user.photo.Count == 0)
            {
                photo.IsMain = true;
            }

            user.photo.Add(photo);
            if (await _userRepository.SaveAllAsync())
            {
                //return _mapper.Map<PhotoDto>(photo);
                return CreatedAtRoute("GetUser", new { username = user.UserName }, _mapper.Map<PhotoDto>(photo));
            }

            return BadRequest("Problem Adding Photo");
        }

        [HttpPut("set-main-photo/{photoID}")]
        public async Task<ActionResult> SetMainPhoto(int photoId)
        {
            var user = await _userRepository.GetUserByUsernameAsync(User.GetUserName());
            var photo = user.photo.FirstOrDefault(x => x.ID == photoId);

            if (photo.IsMain) return BadRequest("This is already your main photo");

            var currentMain = user.photo.FirstOrDefault(x => x.IsMain);
            if (currentMain != null) currentMain.IsMain = false;

            photo.IsMain = true;

            if (await _userRepository.SaveAllAsync()) return NoContent();
            return BadRequest("Failed to set main photo");
        }

        [HttpDelete("delete-photo/{photoID}")]
        public async Task<ActionResult> DeletePhoto(int photoId)
        {
            var user = await _userRepository.GetUserByUsernameAsync(User.GetUserName());
            var photo = user.photo.FirstOrDefault(x => x.ID == photoId);

            if (photo == null) return NotFound();

            if (photo.IsMain) return BadRequest("You cannot delete your main photo");

            if (photo.PublicId != null)
            {
                var result = await _photoService.DeletePhotoAsync(photo.PublicId);

                if (result.Error != null) return BadRequest(result.Error.Message);
            }

            user.photo.Remove(photo);

            if (await _userRepository.SaveAllAsync()) return NoContent();
            return BadRequest("Failed to Delete the Photo");
        }
    }
}