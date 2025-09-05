using Microsoft.AspNetCore.Mvc;
//using poe_part_one.Models;
using poe_partone.Models;

namespace poe_part_one.Controllers
{
    public class HomeController : Controller
    {
        // Fake in-memory storage
        private static List<User> Users = new List<User>
        {
            new User { Email = "lecturer@test.com", Password = "123", Role = "Lecturer" },
            new User { Email = "coordinator@test.com", Password = "123", Role = "Coordinator" },
            new User { Email = "manager@test.com", Password = "123", Role = "Manager" }
        };

        public IActionResult Index()
        {
            return RedirectToAction("Register");
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
    }
}
