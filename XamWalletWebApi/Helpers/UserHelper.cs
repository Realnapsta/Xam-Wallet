using System.Security.Claims;

namespace XamWalletWebApi.Helpers
{
    public class UserHelper
    {
        public static string GetUserId(ClaimsPrincipal User)
        {
            return User.FindFirst("uid")?.Value;
        }

        public static string GetUserEmail(ClaimsPrincipal User)
        {
            return User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;
        }
    }
}
