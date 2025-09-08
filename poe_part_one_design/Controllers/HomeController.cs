using Microsoft.AspNetCore.Mvc;
using poe_part_one.Models;
using Microsoft.AspNetCore.Http;
using poe_part_one_design.Models;

namespace poe_part_one.Controllers
{
    public class HomeController : Controller
    {
        // Fake in-memory storage
        private static List<User> Users = new List<User>
        {
            new User { FirstName = "John", Surname = "Doe", Email = "lecturer@test.com", Password = "123", Role = "Lecturer" },
            new User { FirstName = "Jane", Surname = "Smith", Email = "coordinator@test.com", Password = "123", Role = "Coordinator" },
            new User { FirstName = "Admin", Surname = "User", Email = "manager@test.com", Password = "123", Role = "Manager" }
        };

        // Store claims in memory
        private static List<ClaimSubmission> Claims = new List<ClaimSubmission>
        {
            new ClaimSubmission { LectureID = "1001", HoursWorked = 10, HourlyRate = 400, AdditionalNotes = "Test Data", SupportingDocuments = "Class Register.pdf" }
        };

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
        public IActionResult Register(string firstName, string surname, string email, string password, string role)
        {
            Users.Add(new User
            {
                FirstName = firstName,
                Surname = surname,
                Email = email,
                Password = password,
                Role = role
            });

            HttpContext.Session.SetString("FirstName", firstName);
            HttpContext.Session.SetString("Surname", surname);
            HttpContext.Session.SetString("Email", email);

            return RedirectToAction("Registration_Success");
        }

        public IActionResult Registration_Success()
        {
            var firstName = HttpContext.Session.GetString("FirstName");
            var surname = HttpContext.Session.GetString("Surname");
            ViewBag.Name = $"{firstName} {surname}";
            return View();
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
                HttpContext.Session.SetString("FirstName", user.FirstName);
                HttpContext.Session.SetString("Surname", user.Surname);
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

        // New actions for claim submission
        [HttpPost]
        public IActionResult SubmitClaim(string lectureId, int hoursWorked, decimal hourlyRate, string additionalNotes)
        {
            var claim = new ClaimSubmission
            {
                LectureID = lectureId,
                HoursWorked = hoursWorked,
                HourlyRate = hourlyRate,
                AdditionalNotes = additionalNotes,
                SupportingDocuments = "Class Register.pdf" // Hardcoded for now
            };

            Claims.Add(claim);

            // Store in session for display
            HttpContext.Session.SetString("LastLectureID", lectureId);
            HttpContext.Session.SetInt32("LastHoursWorked", hoursWorked);
            HttpContext.Session.SetString("LastHourlyRate", hourlyRate.ToString());
            HttpContext.Session.SetString("LastAdditionalNotes", additionalNotes);

            return RedirectToAction("Claim_Success");
        }

        public IActionResult Claim_Success()
        {
            ViewBag.LectureID = HttpContext.Session.GetString("LastLectureID");
            ViewBag.HoursWorked = HttpContext.Session.GetInt32("LastHoursWorked");
            ViewBag.HourlyRate = HttpContext.Session.GetString("LastHourlyRate");
            ViewBag.AdditionalNotes = HttpContext.Session.GetString("LastAdditionalNotes");
            ViewBag.SupportingDocuments = "Class Register.pdf";

            return View();
        }

        [HttpGet]
        public IActionResult Track_Claim()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Track_Claim(string lectureId)
        {
            var claim = Claims.FirstOrDefault(c => c.LectureID == lectureId);
            if (claim != null)
            {
                ViewBag.LectureID = claim.LectureID;
                ViewBag.HoursWorked = claim.HoursWorked;
                ViewBag.HourlyRate = claim.HourlyRate;
                ViewBag.AdditionalNotes = claim.AdditionalNotes;
                ViewBag.SupportingDocuments = claim.SupportingDocuments;
                ViewBag.Status = "Pending";
            }
            else
            {
                ViewBag.Error = "Claim not found with the provided Lecture ID";
            }

            return View();
        }
    }
}