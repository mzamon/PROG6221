using Microsoft.AspNetCore.Mvc;

namespace CSharpProject.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}