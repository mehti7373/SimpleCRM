using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Services.Extensions
{
    public static class IdentityExtensions
    {
        private static string FindFirstValue(this ClaimsIdentity claimsIdentity, string claimType)
        {
            if (claimsIdentity == null)
            {
                throw new ArgumentNullException(nameof(claimsIdentity));
            }
            var claim = claimsIdentity.FindFirst(claimType);
            return claim?.Value;
        }

        public static int GetUserId(this IIdentity identity)
        {
            if (identity == null)
            {
                throw new NullReferenceException("identity");
            }
            if (identity is ClaimsIdentity ci)
            {
                var value = ci.FindFirstValue(ClaimTypes.NameIdentifier);
                if (value != null)
                {
                    return Convert.ToInt32(value);
                }
            }

            throw new NullReferenceException(nameof(identity));
        }

    }
}
