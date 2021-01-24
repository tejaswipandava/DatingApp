using System;
using API.data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BuggyController : BaseApiController
    {
        private readonly dataContext _context;
        public BuggyController(dataContext context)
        {
            _context = context;

        }

        [HttpGet("Auth")]
        [Authorize]
        public ActionResult<String> GetSecret()
        {
            return "secret Text";
        }

        [HttpGet("Not-Found")]
        public ActionResult<appUser> GetNotFound()
        {
            var thing = _context.Users.Find(-1);
            if(thing == null) return NotFound();
            return Ok(thing);
        }  

        [HttpGet("Server-Error")]
        public ActionResult<String> GetServerError()
        {
            var thing = _context.Users.Find(-1);
            var thingToReturn = thing.ToString();
            return thingToReturn;
        }        
        
        [HttpGet("Bad-Request")]
        public ActionResult<String> GetBadRequest()
        {
            return BadRequest("This was not a good request");
        }
    }
}