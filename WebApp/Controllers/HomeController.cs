using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using WebApp.ViewModels;
using Data.DatabaseContext;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        const string SessionName = "_Name";
        const string SessionAge = "_Age";

        private readonly CRMDbContext _context;


        public HomeController(CRMDbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {

            return View();
        }

        //[Authorize]
        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        //public async Task<IActionResult> Login(int? login)
        //{
        //    //if (login == null)
        //    //{
        //    //    return NotFound();
        //    //}
        //    var x = _context.Role.Any(m => m.Id == login);
        //    if (x) {
        //        return RedirectToAction("Index", "Users");
        //    }
        //    return View();

        //    //if (user == null)
        //    //{
        //    //    return NotFound();
        //    //}


        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
