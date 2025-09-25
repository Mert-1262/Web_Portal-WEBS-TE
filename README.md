# 🌐 Web Portal (ASP.NET Core MVC)

This is a **Web Portal project** built with **ASP.NET Core MVC**.  
It demonstrates a modular architecture with Controllers, Models, Views, and EF Core integration.

This project is a Web Portal application developed using ASP.NET Core MVC.
It provides a modular structure where users can interact with different modules, manage dynamic data, and experience a modern, standards-compliant web application.

The application follows the Model-View-Controller (MVC) architecture, making the development and maintenance processes more manageable.
It uses Entity Framework Core for database operations, while configuration management is handled through appsettings.json files.

🔹 Key Features

User management with authentication & authorization

Database integration with Entity Framework Core

Dynamic user interfaces built with Razor Views

Static content support (CSS, JS, images) via wwwroot

Modular structure with Controllers and Models

Multi-layered architecture with flexible configuration

🔹 Technologies Used

Backend: ASP.NET Core 6.0 (MVC)

Frontend: Razor Pages, HTML5, CSS3, JavaScript

ORM: Entity Framework Core

Database: Microsoft SQL Server (can be adapted to SQLite/PostgreSQL)

IDE: Visual Studio 2022




---

## ✨ Features
- MVC architecture with Controllers, Models, and Views
- Authentication & Authorization (if implemented)
- Database integration with Entity Framework Core
- Static content served via `wwwroot/`
- Configurable environment settings via `appsettings.json`

---
Web_Portal/
├─ Controllers/ # MVC Controllers
├─ Models/ # Data models
├─ Views/ # Razor views
├─ Data/ # DbContext and EF migrations
├─ wwwroot/ # Static files (css, js, images)
├─ appsettings.json # Configuration
├─ Program.cs # Main entry point
└─ Web_Portal.csproj
---

---

## ⚙️ Requirements
- .NET 6 (or compatible SDK)
- Visual Studio 2022 / VS Code
- SQL Server or SQLite

---

## 🚀 Run Locally
bash
git clone https://github.com/kullaniciadi/web-portal.git
cd Web_Portal
dotnet restore
dotnet run
---
The app will be available at:
👉 https://localhost:5001 or http://localhost:5000

📝 Notes

Update appsettings.json with your database connection string.

Do not commit sensitive credentials.

📜 License

MIT
