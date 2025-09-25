using Microsoft.AspNetCore.Mvc;
using Web_Portal.Data;
using Web_Portal.Models;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Web_Portal.Controllers
{
    public class AdminLoginController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminLoginController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var admin = _context.Admins.FirstOrDefault(a => a.Email == email && a.Password == password);

            if (admin != null)
            {
                HttpContext.Session.SetInt32("Admin_ID", admin.Admin_ID);
                HttpContext.Session.SetInt32("Company_ID", admin.Company_ID);

                return RedirectToAction("Dashboard", "Admin");
            }

            ViewBag.Error = "Hatalı email veya şifre!";
            return View("Index");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
