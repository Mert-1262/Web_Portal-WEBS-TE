using Microsoft.EntityFrameworkCore;
using Web_Portal.Data;

var builder = WebApplication.CreateBuilder(args);

// 📌 Veritabanı bağlantısını ekle
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllersWithViews();
builder.Services.AddSession();  // 📌 Kullanıcı oturumları için ekle

var app = builder.Build();

// 📌 Geliştirme ortamı kontrolü
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseSession(); // 📌 Kullanıcı oturumlarını aktif hale getir

// 📌 Varsayılan rotayı ayarla
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=AdminLogin}/{action=Index}/{id?}");

app.Run();
