using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.data
{
    public class dataContext : DbContext
    {
        public dataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<appUser> Users { get; set; }
    }
}