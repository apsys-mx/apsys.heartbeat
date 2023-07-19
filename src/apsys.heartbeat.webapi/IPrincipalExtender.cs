using System.Security.Claims;
using System.Security.Principal;

namespace apsys.heartbeat.webapi
{
    /// <summary>
    /// IPrincipalExtender class
    /// </summary>
    public static class IPrincipalExtender
    {
        /// <summary>
        /// Get a claim from iprincipal 
        /// </summary>
        /// <param name="principal"></param>
        /// <param name="claimType"></param>
        /// <returns></returns>
        public static Claim? GetClaim(this IPrincipal principal, string claimType)
        {
            ClaimsPrincipal? claims = principal as ClaimsPrincipal;
            return claims?.FindFirst(claimType);
        }

        /// <summary>
        /// Get a claim value
        /// </summary>
        /// <param name="principal"></param>
        /// <param name="claimType"></param>
        /// <returns></returns>
        public static string GetClaimValue(this IPrincipal principal, string claimType)
        {
            var claimn = GetClaim(principal, claimType);
            return claimn == null ? string.Empty : claimn.Value;
        }

        /// <summary>
        /// Get the username from the iprincipal claims
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        public static string GetUserName(this IPrincipal principal) => GetClaimValue(principal, "user_name");

        /// <summary>
        /// Get the username from the iprincipal claims
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        public static string GetName(this IPrincipal principal) => GetClaimValue(principal, "name");

        /// <summary>
        /// Get the user type from the claims
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        public static string GetUserType(this IPrincipal principal) => GetClaimValue(principal, "user_type");
    }
}
