using Data.Enums;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Utility
{
    public class CookieAuthorizeAttribute : CustomAuthorizeAttribute
    {
        public CookieAuthorizeAttribute(params Role[] roles)
            : base(roles)
        {
            AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme;
        }
    }

    public abstract class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public CustomAuthorizeAttribute(params Role[] roles)
        {
            Roles = string.Join(",", roles.Select(r => r.ToString()));
        }
    }
    
}
