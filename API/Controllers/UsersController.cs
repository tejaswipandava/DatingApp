using System.Collections.Generic;
using System.Threading.Tasks;
using API.data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : BaseApiController
    {
        private readonly dataContext _context;
        public UsersController(dataContext context)
        {
            _context = context;
        }

        //[Authorize]
        [HttpGet("getUsers")]
        public async Task<ActionResult<IEnumerable<appUser>>> getUsers()
        {
            return await _context.Users.ToListAsync();
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<appUser>> getUser(int id)
        {
            return await _context.Users.FindAsync(id);
        }
    }
}