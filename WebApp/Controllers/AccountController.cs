using Data.DatabaseContext;
using Data.Entities;
using Data.Enums;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly CRMDbContext _dbContext;
        private readonly ICookieProvider _cookieProvider;
        private readonly IEncrypter _encrypter;
        public AccountController(CRMDbContext crmDbContext, ICookieProvider cookieProvider, IEncrypter encrypter)
        {
            _dbContext = crmDbContext;
            _cookieProvider = cookieProvider;
            _encrypter = encrypter;
        }
        [Route("account/login")]
        [AllowAnonymous]
        public IActionResult Login(string ReturnUrl)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return Redirect("/");
            }
            ViewData["ReturnUrl"] = ReturnUrl;
            return View();
        }
        [HttpPost]
        [Route("account/login")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string ReturnUrl, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _dbContext.Users.FirstOrDefaultAsync(a => a.Email == model.Email);

            if (user == null)
            {
                ModelState.AddModelError("", "کاربر یافت نشد");
                return View(model);
            }

            var isValid = _encrypter.ValidateHash(user.PasswordHash, model.Password, user.Salt);
            if (!isValid)
            {
                ModelState.AddModelError("", "کاربر یافت نشد");
                return View(model);
            }
            var roles = new List<Role> { user.Role };

            var cookieClaims = _cookieProvider.CreateCookieClaims(user, rolesName(roles));

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                cookieClaims,
                 new AuthenticationProperties
                 {
                     IsPersistent = model.RemmemberMe,//remember me
                     IssuedUtc = DateTimeOffset.UtcNow,
                     ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)//.AddDays(loginCookieExpirationDays)
                 }
                );

            return RedirectToLocal(ReturnUrl);


        }

        [Route("account/register")]
        [AllowAnonymous]
        public IActionResult Register(string ReturnUrl)
        {
            ViewData["ReturnUrl"] = ReturnUrl;
            return View();
        }

        [Route("account/register")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken, HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model, string ReturnUrl, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _dbContext.Users.FirstOrDefaultAsync(a => a.Email == model.Email);

            if (user != null)
            {
                ModelState.AddModelError("", "ایمیل قبلا ثبت شده است");
                return View(model);
            }

            var salt = _encrypter.GetSalt();
            var userNew = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                CreateAt = DateTime.Now,
                LockoutEnabled = false,
                Role = Data.Enums.Role.User,
                Salt = salt,
                PasswordHash = _encrypter.GetHash(model.Password, salt)
            };
            _dbContext.Add(userNew);

            await _dbContext.SaveChangesAsync();

            var roles = new List<Role> { userNew.Role };
            var cookieClaims = _cookieProvider.CreateCookieClaims(userNew, rolesName(roles));

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                cookieClaims,
                 new AuthenticationProperties
                 {
                     IsPersistent = false,//remember me
                     IssuedUtc = DateTimeOffset.UtcNow,
                     ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)//.AddDays(loginCookieExpirationDays)
                 }
                );

            return RedirectToLocal(ReturnUrl);


        }

        [Route("account/logout")]
        [AllowAnonymous]
        public async Task<IActionResult> Logout(CancellationToken cancellationToken)
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme
                );
            return RedirectToAction("login", "Account", "");
        }
        private string[] rolesName(IEnumerable<Role> userRoles)
        {
            string[] names = new string[userRoles.Count()];
            for (int i = 0; i < names.Count(); i++)
            {
                var userRole = userRoles.ElementAt(i);
                names[i] = userRole.ToString();
            }

            return names;
        }
        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

    }
}
