using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly iUserRepository _userRepository;
        public UsersController(iUserRepository userRepository)
        {
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
        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>> getUser(string username)
        {
            // var user = await _userRepository.GetUserByUsernameAsync(username);
            return await _userRepository.GetMemberAsync(username);
        }
    }
}