using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using navigation_mvc.Models;

namespace navigation_mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy(string username)
        {
            //store the viewbagin a colum called user
            ViewBag.user = username;
            //example of manual store
            ViewBag.ex = "Not auto";
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
