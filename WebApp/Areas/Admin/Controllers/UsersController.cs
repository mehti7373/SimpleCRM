using Data.DatabaseContext;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;
using Services.Dtos;
using Services.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Areas.Admin.Models;

namespace WebApp.Areas.Admin.Controllers
{
    public class UsersController : AdminController
    {
        private readonly CRMDbContext _dbContext;
        private readonly IEncrypter _encrypter;

        public UsersController(CRMDbContext dbContext, IEncrypter encrypter)
        {
            _dbContext = dbContext;
            _encrypter = encrypter;
        }

        public async Task<IActionResult> ChangePassword(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var user = await _dbContext.Users.FindAsync(id.Value);

            if (user == null)
            {
                return NotFound();
            }

            var model = new UserChangePasswordViewModel()
            {
                Id = user.Id
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(UserChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _dbContext.Users.FindAsync(model.Id);

            user.PasswordHash = _encrypter.GetHash(model.Password, user.Salt);
            _dbContext.Update(user);

            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
            //}

            //foreach (var err in result.Errors)
            //{
            //    ModelState.AddModelError("", err);
            //}

        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var existUser =await _dbContext.Users.AnyAsync(a => a.Email == model.Email);
            if (existUser)
            {
                ModelState.AddModelError("", "ایمیل قبلا ثبت شده است");
                return View(model);
            }
            var salt = _encrypter.GetSalt();
            var user = new User
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
            _dbContext.Add(user);

            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null)
            {
                return BadRequest();
            }

            var user = await _dbContext.Users.FindAsync(id.Value);
            if (user == null)
            {
                return NotFound();
            }
            var model = new UserEditViewModel
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                LockoutEnabled = user.LockoutEnabled,
                Role = user.Role
            };

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _dbContext.Users.FindAsync(model.Id);

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.LockoutEnabled = model.LockoutEnabled;
            user.Role = model.Role;
            _dbContext.Update(user);

            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Index(int page = 1, string sortExpression = "Id")
        {

            var qry = _dbContext.Users.AsNoTracking().OrderBy(p => p.Id)
                .Select(a => new UserIndexViewModel
                {
                    Id = a.Id,
                    Email = a.Email,
                    CreateAt = a.CreateAt,
                    Role = a.Role
                });
            var model = await PagingList.CreateAsync(qry, 20, page, sortExpression, "Id");
            string[] roles = { "Admin", "Moderator", "User" };
            ViewBag.roles = roles;
            return View(model);


            //var q = _dbContext.Users.Where(a => true);
            //var pager = new PagerDTO(await q.CountAsync(), page);

            //ViewBag.Pager = pager;

            //var model = await
            //    q
            //    //.GetAllByRoleAsync<UserIndexViewModel>(null, name, email, pager)
            //    .ToListAsync();

            //return View(model);
        }
    }
}
