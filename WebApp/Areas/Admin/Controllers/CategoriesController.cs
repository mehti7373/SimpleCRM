using Data.DatabaseContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Areas.Admin.Controllers
{
    public class CategoriesController : AdminController
    {
        private readonly CRMDbContext _dbContext;

        public CategoriesController(CRMDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Data.Entities.Category model)
        {
            model.CreateAt = DateTime.Now;
            _dbContext.Add(model);

            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var category = await _dbContext.Categories.FindAsync(id.Value);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Data.Entities.Category model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var category = await _dbContext.Categories.FindAsync(model.Id);
            category.Title = model.Title;

            _dbContext.Update(category);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Index(int page = 1, string sortExpression = "Id")
        {
            var qry = _dbContext.Categories.AsNoTracking().OrderBy(p => p.Id);
            var model = await PagingList.CreateAsync(qry, 20, page, sortExpression, "Id");
            return View(model);

        }
    }
}
