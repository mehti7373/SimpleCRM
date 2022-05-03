using Data.DatabaseContext;
using Data.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;
using Services.Extensions;
using Services.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Areas.Admin.Models;
using WebApp.Utility;

namespace WebApp.Areas.Admin.Controllers
{
    [CookieAuthorize(roles:new Role[] {Role.Administrator,Role.Supporter })]
    public class TicketsController : AdminController
    {

        private readonly IFileService _fileService;
        private readonly CRMDbContext _dbContext;

        public TicketsController(CRMDbContext dbContext, IFileService fileService)
        {
            _dbContext = dbContext;
            _fileService = fileService;
        }

        public async Task<IActionResult> Details(int id)
        {
            var model = await _dbContext.Tickets.AsNoTracking().Where(a => a.Id == id)
                 .OrderBy(p => p.Id)
                 .Select(a => new TicketDetailsViewModel
                 {
                     Id = a.Id,
                     Title = a.Title,
                     CategoryTitle = a.Category.Title,
                     CreateAt = a.CreateAt,
                     IsOpen = a.IsOpen,
                     TicketMessages = a.TicketMessages

                 }).SingleOrDefaultAsync();

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Reply(TicketReplyViewModel model)
        {
            var ticket = await _dbContext.Tickets.FindAsync(model.Id);
            string file = null;
            if (model.FormFile is not null)
            {
                file = await _fileService.SaveTicketFilesAsync(model.FormFile);
            }

            ticket.AdminRepliedUserId = HttpContext.User.Identity.GetUserId();
            ticket.ReplyAt = DateTime.Now;
            ticket.IsOpen = false;
            _dbContext.Update(ticket);

            _dbContext.TicketMessages.Add(new Data.Entities.TicketMessage
            {
                CreateAt = DateTime.Now,
                Message = model.Message,
                TicketId = ticket.Id,
                TicketSide = Data.Enums.TicketSide.AnswerAdmin,
                UserId = HttpContext.User.Identity.GetUserId(),
                File = file
            });
            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { Id = model.Id });
        }


        public async Task<IActionResult> Index(int page = 1, string sortExpression = "Id")
        {
            var qry = _dbContext.Tickets.AsNoTracking()
                //.Include(a=>a.Category)
                .OrderBy(p => p.Id)
                .Select(a => new TicketIndexViewModel
                {
                    Id = a.Id,
                    Title = a.Title,
                    CategoryTitle = a.Category.Title,
                    CreateAt = a.CreateAt,
                    IsDone = !a.IsOpen,
                    UserId = a.UserId,
                });
            var model = await PagingList.CreateAsync(qry, 6, page, sortExpression, "Id");

           
            return View(model);
        }
    }
}
