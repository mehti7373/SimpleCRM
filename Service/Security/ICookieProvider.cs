using Data.Entities;
using System.Security.Claims;

namespace Services.Security
{
    public interface ICookieProvider
    {
        ClaimsPrincipal CreateCookieClaims(User user, params string[] roles);
    }
}