using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using poe_part_one.Models;
using Microsoft.AspNetCore.Http;
using poe_part_one_design.Models;

namespace poe_part_one.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        // Fake in-memory storage
        private static List<User> Users = new List<User>
        {
            new User { Email = "lecturer@test.com", Password = "123", Role = "Lecturer" },
            new User { Email = "coordinator@test.com", Password = "123", Role = "Coordinator" },
            new User { Email = "manager@test.com", Password = "123", Role = "Manager" }
        };

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // Check if user is already logged in
            var role = HttpContext.Session.GetString("Role");
            if (role != null)
            {
                return RedirectToAction("Dashboard");
            }
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public IActionResult Register(string email, string password, string role)
        {
            Users.Add(new User { Email = email, Password = password, Role = role });
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var user = Users.FirstOrDefault(u => u.Email == email && u.Password == password);
            if (user != null)
            {
                HttpContext.Session.SetString("Role", user.Role);
                HttpContext.Session.SetString("Email", user.Email);
                return RedirectToAction("Dashboard");
            }
            ViewBag.Error = "Invalid credentials!";
            return View();
        }

        public IActionResult Dashboard()
        {
            var role = HttpContext.Session.GetString("Role");
            if (role == null) return RedirectToAction("Login");

            ViewBag.Role = role;
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        // Partials
        public IActionResult LecturerPartial() => PartialView("Lecturer_View");
        public IActionResult CoordinatorPartial() => PartialView("Coordinator_View");
        public IActionResult ManagerPartial() => PartialView("Manager_View");

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}