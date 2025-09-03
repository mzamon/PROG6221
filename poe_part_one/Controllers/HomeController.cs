using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using poe_part_one.Models;

namespace poe_part_one.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Privacy()
        {
            return View();
        }

        //This is for logging in
        public IActionResult Index(string username, string password)
        {
            if (username == "admin" && password == "admin")
            {
                return RedirectToAction("Home");
            }

            ViewBag.Error = "Icorrect combination of username and/or password! PLEASE USE [admin] + (admin)";
            return View();
        }
        //Views after logging in
        public IActionResult Home()
        {
            return View();
        }
        //Partial to open Lecturer View
        public IActionResult LecturerPartial()
        {
            return PartialView("Lecturer_View");
        }
        //Partial to open Coordinator View
        public IActionResult CoordinatorPartial()
        {
            return PartialView("Coordinator_View");
        }
        //Partial ro logout
        public IActionResult LogoutPartial()
        {
            return RedirectToAction("Index", "Home");
            //return PartialView("Index", "Home");
            /*Alternative way to logout
             * return RedirectToAction("Index");
             */
        }
        //Partial to open Manager View
        public IActionResult ManagerPartial()
        {
            return PartialView("Manager_View");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
