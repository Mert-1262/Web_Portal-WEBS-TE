using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_Portal.Data;
using Web_Portal.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Web_Portal.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ApplicationDbContext _context;
        private const int PageSize = 5; // 📌 Sayfa başına 5 şirket gösterilecek

        public CompanyController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 📌 Şirketleri Listele (Arama ve Sayfalama ile)
        public async Task<IActionResult> Index(string searchName, string searchAddress, int page = 1)
        {
            var companies = _context.Companies.AsQueryable();

            // 📌 Arama Filtreleme
            if (!string.IsNullOrEmpty(searchName))
            {
                companies = companies.Where(c => c.company_name.Contains(searchName));
            }
            if (!string.IsNullOrEmpty(searchAddress))
            {
                companies = companies.Where(c => c.address.Contains(searchAddress));
            }

            int totalCompanies = await companies.CountAsync(); // 📌 Toplam şirket sayısını al
            int totalPages = (int)System.Math.Ceiling((double)totalCompanies / PageSize); // 📌 Kaç sayfa olmalı?

            var paginatedCompanies = await companies
                .Skip((page - 1) * PageSize) // 📌 Sayfa başına atlama işlemi
                .Take(PageSize) // 📌 Belirtilen sayıda şirket getir
                .ToListAsync();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

            ViewBag.SearchName = searchName;
            ViewBag.SearchAddress = searchAddress;

            return View(paginatedCompanies);
        }

        // 📌 Yeni Şirket Ekleme Sayfası
        public IActionResult Create()
        {
            return View();
        }

        // 📌 Yeni Şirket Ekleme İşlemi
        [HttpPost]
        public async Task<IActionResult> Create(Company company)
        {
            if (ModelState.IsValid)
            {
                _context.Companies.Add(company);
                await _context.SaveChangesAsync(); // 🚀 Async olarak kaydet
                return RedirectToAction("Index");
            }
            return View(company);
        }

        // 📌 Şirket Güncelleme Sayfası
        public async Task<IActionResult> Edit(int id)
        {
            var company = await _context.Companies.FindAsync(id); // 🚀 Async Find
            if (company == null) return NotFound();
            return View(company);
        }

        // 📌 Şirket Güncelleme İşlemi
        [HttpPost]
        public async Task<IActionResult> Edit(Company company)
        {
            if (ModelState.IsValid)
            {
                _context.Companies.Update(company);
                await _context.SaveChangesAsync(); // 🚀 Async olarak kaydet
                return RedirectToAction("Index");
            }
            return View(company);
        }

        // 📌 Şirket Silme İşlemi
        public async Task<IActionResult> Delete(int id)
        {
            var company = await _context.Companies.FindAsync(id); // 🚀 Async Find
            if (company != null)
            {
                _context.Companies.Remove(company);
                await _context.SaveChangesAsync(); // 🚀 Async olarak kaydet
            }
            return RedirectToAction("Index");
        }
    }
}
