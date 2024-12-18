using System.Security.Claims;

namespace DatingApi.Extensions
{
    public static class ClaimPrincipalExtensions
    {
        public static string GetUserName(this ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new Exception("can not get user name from token");
        }
    }
}
