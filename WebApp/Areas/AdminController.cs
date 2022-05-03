using Data.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Utility;

namespace WebApp.Areas
{
    [Area("Admin")]
    [CookieAuthorize(roles: Role.Administrator)]
    public abstract class AdminController : Controller
    {
    }
}