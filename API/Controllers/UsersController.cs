using System.Collections.Generic;
using System.Threading.Tasks;
using API.data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly dataContext _context;
        public UsersController(dataContext context)
        {
            _context = context;
        }

        [HttpGet("getUsers")]
        public async Task<ActionResult<IEnumerable<appUser>>> getUsers()
        {
            return await _context.Users.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<appUser>> getUser(int id)
        {
            return await _context.Users.FindAsync(id);
        }
    }
}