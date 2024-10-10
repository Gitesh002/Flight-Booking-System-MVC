using Microsoft.AspNetCore.Mvc;
using fbs.Models; // Updated namespace
using System.Linq;
using fbs.Data;

namespace fbs.Controllers // Updated namespace
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ------------------------------ admin login ---------------------------------
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var admin = _context.Admins.FirstOrDefault(a => a.Username == username && a.Password == password);
            if (admin != null)
            {
                HttpContext.Session.SetString("Username", admin.Username);
                return RedirectToAction("AdminMenu", "Admin"); // Redirect to flight listing after login
            }
            ModelState.AddModelError("", "Invalid username or password");
            return View();
        }

        public IActionResult AdminMenu()
        {
            return View();
        }

        //------------------------------- flight-----------------------------------
        public IActionResult CreateFlight()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateFlight(Flight flight)
        {
            if (ModelState.IsValid)
            {
                _context.Flights.Add(flight);
                _context.SaveChanges();
                return RedirectToAction("Index", "Flight"); // Redirect to flight listing after adding
            }
            return View(flight);
        }
    }
}
