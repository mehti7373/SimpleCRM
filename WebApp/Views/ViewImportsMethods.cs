using System;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Data.Enums;
using Services.Dtos;
using Data.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Views
{
    public abstract class ViewImportsMethods<TModel> : RazorPage<TModel>
    {
        public bool IsAuthenticated()
        {
            return Context.User.Identity.IsAuthenticated;
        }

        public bool UserIsAdmin()
        {
            return
                Context.User.Claims
                .Where(c => c.Type == ClaimTypes.Role && c.Value == Role.Administrator.ToString())
                .Any();
        }
        public bool UserIsSupporter()
        {
            return
                Context.User.Claims
                .Where(c => c.Type == ClaimTypes.Role && c.Value == Role.Supporter.ToString())
                .Any();
        }
        public string UserName()
        {
            return Context.User.Identity.Name;
        }

        public async Task<IEnumerable<BaseDTO<int, string>>> GetAllCategoris()
        {
            var experienceAcceleratorService = Context.RequestServices.GetRequiredService<CRMDbContext>();

            var dtos = await experienceAcceleratorService.Categories.Select(a => new BaseDTO<int, string>
            {
                Id = a.Id,
                Title = a.Title

            }).ToListAsync();

            return dtos;
        }

        public IEnumerable<BaseDTO<int, string>> GetRoleList()
        {
            return Enum.GetValues(typeof(Role)).Cast<Role>().Select(
                r => new BaseDTO<int, string>
                {
                    Id = (int)r,
                    Title = $"{r}",
                }
            ) ;
        }
    }
}
