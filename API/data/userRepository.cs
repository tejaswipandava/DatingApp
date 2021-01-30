using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.data
{
    public class userRepository : iUserRepository
    {
        private readonly dataContext _context;
        private readonly IMapper _mapper;
        public userRepository(dataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;

        }

        public async Task<MemberDto> GetMemberAsync(string username)
        {
            return await _context.Users
            .Where(x =>x.UserName == username)
            .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<MemberDto>> GetMembersAsync()
        {
            return await _context.Users.ProjectTo<MemberDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<appUser> GetUserByIdAsync(int ID)
        {
            return await _context.Users.FindAsync(ID);
        }

        public async Task<appUser> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.Include(p => p.photo).SingleOrDefaultAsync(uname => uname.UserName == username);
        }

        public async Task<IEnumerable<appUser>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(appUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }
    }
}