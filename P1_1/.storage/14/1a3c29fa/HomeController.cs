using Microsoft.AspNetCore.Mvc;

namespace CSharpProject.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SubmitClaim()
        {
            // Advanced C# logic for claim submission can be added here
            return RedirectToAction("Index");
        }
    }
}