using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Web_Portal.Data;
using Web_Portal.Models;
using System.Linq;

namespace Web_Portal.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Dashboard()
        {
            // 📌 Eğer oturum açılmamışsa, giriş sayfasına yönlendir.
            if (HttpContext.Session.GetInt32("Admin_ID") == null)
            {
                return RedirectToAction("Index", "AdminLogin");
            }

            // 📌 Giriş yapan Admin'in bilgilerini getir.
            int adminId = (int)HttpContext.Session.GetInt32("Admin_ID");
            var admin = _context.Admins.FirstOrDefault(a => a.Admin_ID == adminId);

            if (admin == null)
            {
                // Eğer Admin veritabanında bulunamazsa oturumu temizleyip giriş sayfasına yönlendir.
                HttpContext.Session.Clear();
                return RedirectToAction("Index", "AdminLogin");
            }

            ViewBag.AdminName = admin.Email;
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "AdminLogin"); // 📌 Çıkış sonrası giriş sayfasına yönlendir.
        }
    }
}
