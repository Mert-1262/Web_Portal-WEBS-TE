using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Web_Portal.Models;

namespace Web_Portal.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Admin> Admins { get; set; } // 🚨 `Admin_List` yerine `Admins` olmalı, çünkü modelde `[Table("Admin_List")]` zaten var.
        public DbSet<Company> Companies { get; set; } // `Company_list` yerine `Companies`
        public DbSet<Employee> Employees { get; set; } // `Employee_List` yerine `Employees`

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Admin>().ToTable("Admin_List"); // 📌 Admin_List tablosuna yönlendirildi
            modelBuilder.Entity<Company>().ToTable("Company_List"); // 📌 Company_List tablosuna yönlendirildi
            modelBuilder.Entity<Employee>().ToTable("Employee_List"); // 📌 Employee_List tablosuna yönlendirildi
        }
    }
}
