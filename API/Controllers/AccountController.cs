using System;
using System.Text;
using System.Security.Cryptography;
using System.Threading.Tasks;
using API.data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using API.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using API.Interfaces;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly dataContext _context;
        private readonly iTokenService _tokenService;
        public AccountController(dataContext context, iTokenService tokenService)
        {
            _tokenService = tokenService;
            _context = context;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto newuser)
        {
            if (await UserExists(newuser.username))
            {
                return BadRequest("Username is taken");
            }

            using var hmac = new HMACSHA512();

            var user = new appUser
            {
                UserName = newuser.username,
                PasswordSalt = hmac.Key,
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(newuser.password))
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return new UserDto
            {
                UserName = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }

        private async Task<bool> UserExists(string username)
        {
            return await _context.Users.AnyAsync(x => x.UserName == username.ToLower());
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> login(LoginDto loginUser)
        {
            var user = await _context.Users.Include(p => p.photo).SingleOrDefaultAsync(x => x.UserName == loginUser.username);

            if (user == null) return Unauthorized("Invalid Username");

            using var hmac = new HMACSHA512(user.PasswordSalt);

            Byte[] computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginUser.password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.passwordHash[i]) return Unauthorized("invalid password");
            }

            return new UserDto
            {
                UserName = user.UserName,
                Token = _tokenService.CreateToken(user),
                PhotoUrl = user.photo.FirstOrDefault(x => x.IsMain)?.Url
            };
        }
    }
}