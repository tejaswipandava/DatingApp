using System.Security.Claims;
namespace API.Extensions
{
    public static class ClaimsprincipleExtension
    {
        public static string GetUserName(this ClaimsPrincipal User)
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}