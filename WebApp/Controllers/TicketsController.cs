using Data.DatabaseContext;
using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;
using Services.Extensions;
using Services.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Authorize]
    public class TicketsController : Controller
    {
        private readonly CRMDbContext _dbContext;
        private readonly IFileService _fileService;

        public TicketsController(CRMDbContext dbContext, IFileService fileService)
        {
            _dbContext = dbContext;
            _fileService = fileService;
        }
        public IActionResult Create()
        {
            return View();
        }
        [ValidateAntiForgeryToken, HttpPost]
        public async Task<IActionResult> Create(TicketCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            string file = null;
            if (model.FormFile is not null)
            {
                file = await _fileService.SaveTicketFilesAsync(model.FormFile);
            }
            var ticket = new Data.Entities.Ticket
            {
                CategoryId = model.CategoryId,
                UserId = HttpContext.User.Identity.GetUserId(),
                CreateAt = DateTime.Now,
                IsOpen = true,
                Title = model.Title,
                TicketMessages = new List<TicketMessage> ()
            };

            ticket.TicketMessages.Add(new TicketMessage
            {
                CreateAt = DateTime.Now,
                Message = model.Message,
                TicketSide = Data.Enums.TicketSide.RequestUser,
                UserId = HttpContext.User.Identity.GetUserId(),
                File = file
            });

            _dbContext.Add(ticket);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Index(int page = 1, string sortExpression = "Id")
        {
            var qry = _dbContext.Tickets.AsNoTracking().Where(a => a.UserId == HttpContext.User.Identity.GetUserId())
                 .OrderBy(p => p.Id)
                 .Select(a => new TicketIndexViewModel
                 {
                     Id = a.Id,
                     Title = a.Title,
                     CategoryTitle = a.Category.Title,
                     CreateAt = a.CreateAt,
                     TicketMessages = a.TicketMessages

                 });
            var model = await PagingList.CreateAsync(qry, 20, page, sortExpression, "Id");
            foreach (var item in model)
            {
                item.CreateAtPersian = item.CreateAt.ToPersianDateString();
            }
            return View(model);
        }
    }
}
