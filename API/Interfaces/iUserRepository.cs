using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface iUserRepository
    {
        void Update(appUser user);
        Task<bool> SaveAllAsync();
        Task<IEnumerable<appUser>> GetUsersAsync();
        Task<appUser> GetUserByIdAsync(int ID);
        Task<appUser> GetUserByUsernameAsync(string username);
        Task<IEnumerable<MemberDto>> GetMembersAsync();
        Task<MemberDto> GetMemberAsync(string username);
    }
}