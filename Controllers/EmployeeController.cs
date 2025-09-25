using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Web_Portal.Data;
using Web_Portal.Models;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Web_Portal.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private const int PageSize = 5; // 📌 Sayfa başına 8 personel gösterilecek

        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 📌 SADECE Adminin Şirketindeki Personelleri Listele (Sayfalama ile)
        public async Task<IActionResult> Index(int page = 1)
        {
            int? adminCompanyId = HttpContext.Session.GetInt32("Company_ID");
            if (adminCompanyId == null)
            {
                return RedirectToAction("Index", "AdminLogin");
            }

            var employees = _context.Employees
                .Where(e => e.Company_ID == adminCompanyId)
                .AsQueryable();

            int totalEmployees = await employees.CountAsync(); // 📌 Toplam personel sayısını al
            int totalPages = (int)System.Math.Ceiling((double)totalEmployees / PageSize); // 📌 Kaç sayfa olmalı?

            var paginatedEmployees = await employees
                .Skip((page - 1) * PageSize) // 📌 Sayfa başına atlama işlemi
                .Take(PageSize) // 📌 8 personel getir
                .ToListAsync();

            ViewBag.CurrentPage = page; // 📌 Mevcut sayfa
            ViewBag.TotalPages = totalPages; // 📌 Toplam sayfa sayısı

            return View(paginatedEmployees);
        }

        // 🔹 2️⃣ Yeni Personel Ekleme Sayfası
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            int? adminCompanyId = HttpContext.Session.GetInt32("Company_ID");
            if (adminCompanyId == null)
            {
                TempData["Message"] = "Oturum süresi doldu, lütfen tekrar giriş yapın.";
                TempData["MessageType"] = "error";
                return RedirectToAction("Index", "AdminLogin");
            }

            employee.Company_ID = adminCompanyId.Value;
            _context.Employees.Add(employee);
            _context.SaveChanges();

            // 📌 Başarı mesajı ekleyelim
            TempData["Message"] = "Personel başarıyla eklendi!";
            TempData["MessageType"] = "success";

            return RedirectToAction("Index");
        }


        // 🔹 3️⃣ Personel Güncelleme Sayfası
        public IActionResult Edit(int id)
        {
            int? adminCompanyId = HttpContext.Session.GetInt32("Company_ID");
            if (adminCompanyId == null)
            {
                return RedirectToAction("Index", "AdminLogin");
            }

            var employee = _context.Employees.FirstOrDefault(e => e.Employee_ID == id && e.Company_ID == adminCompanyId);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        [HttpPost]
        public IActionResult Edit(Employee employee)
        {
            int? adminCompanyId = HttpContext.Session.GetInt32("Company_ID");
            if (adminCompanyId == null)
            {
                return RedirectToAction("Index", "AdminLogin");
            }

            employee.Company_ID = adminCompanyId.Value;
            _context.Employees.Update(employee);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            int? adminCompanyId = HttpContext.Session.GetInt32("Company_ID");
            if (adminCompanyId == null)
            {
                TempData["Message"] = "Oturum süresi doldu, lütfen tekrar giriş yapın.";
                TempData["MessageType"] = "error";
                return RedirectToAction("Index", "AdminLogin");
            }

            var employee = _context.Employees.FirstOrDefault(e => e.Employee_ID == id && e.Company_ID == adminCompanyId);
            if (employee == null)
            {
                TempData["Message"] = "Personel bulunamadı!";
                TempData["MessageType"] = "error";
                return RedirectToAction("Index");
            }

            _context.Employees.Remove(employee);
            _context.SaveChanges();

            // 📌 Başarı mesajı
            TempData["Message"] = "Personel başarıyla silindi.";
            TempData["MessageType"] = "warning";

            return RedirectToAction("Index");
        }


        [HttpGet] // 🔹 Bu metodun sadece GET isteği almasını sağlıyoruz

        public async Task<IActionResult> Index(string searchName, string searchEmail, string searchPhone, string searchAddress, int page = 1)
        {
            int? adminCompanyId = HttpContext.Session.GetInt32("Company_ID");
            if (adminCompanyId == null)
            {
                return RedirectToAction("Index", "AdminLogin");
            }

            var employees = _context.Employees
                .Where(e => e.Company_ID == adminCompanyId)
                .AsQueryable();

            // 📌 Arama kriterlerini uygula
            if (!string.IsNullOrEmpty(searchName))
            {
                employees = employees.Where(e => e.Full_Name.Contains(searchName));
            }
            if (!string.IsNullOrEmpty(searchEmail))
            {
                employees = employees.Where(e => e.Email.Contains(searchEmail));
            }
            if (!string.IsNullOrEmpty(searchPhone))
            {
                employees = employees.Where(e => e.Phone.Contains(searchPhone));
            }
            if (!string.IsNullOrEmpty(searchAddress))
            {
                employees = employees.Where(e => e.Address.Contains(searchAddress));
            }

            int totalEmployees = await employees.CountAsync();
            int totalPages = (int)System.Math.Ceiling((double)totalEmployees / PageSize);

            var paginatedEmployees = await employees
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

            // 📌 View'a filtre değerlerini geri gönderelim ki input kutularında kalsın
            ViewBag.SearchName = searchName;
            ViewBag.SearchEmail = searchEmail;
            ViewBag.SearchPhone = searchPhone;
            ViewBag.SearchAddress = searchAddress;

            return View(paginatedEmployees);
        }



    }
}
