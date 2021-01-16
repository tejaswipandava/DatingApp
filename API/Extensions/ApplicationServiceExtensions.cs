using API.data;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection addApplicationService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<dataContext>(options => 
            {
                options.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<iTokenService, TokenService>();

            return services;
        }
    }
}