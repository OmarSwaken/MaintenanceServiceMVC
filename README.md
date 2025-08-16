# 🛠 MaintenanceServiceMVC - Project Setup

## Project Overview

**MaintenanceServiceMVC** is a web application designed to connect **customers** with **professionals** for various services such as plumbing, electrical work, carpentry, cleaning, and painting.  

**Key features:**

- Customer registration and service requests  
- Professional management with ratings, specialties, and availability  
- Service request tracking (Pending, Assigned, InProgress, Completed, Cancelled)  
- Reviews and ratings for professionals  
- Seeded sample data for immediate testing  

This project is built with **ASP.NET Core MVC** and **Entity Framework Core** using **SQL Server LocalDB**.

---

## 1. Requirements

Before running the project, ensure you have:

- **Visual Studio** (with ASP.NET and EF Core tools installed)  
- **SQL Server LocalDB** (usually comes with Visual Studio)  
- **.NET SDK** (same version as in the project, e.g., .NET 8.0)  

---

## 2. Database Setup

This project uses **Entity Framework Core Migrations** to create the database and seed initial data.

### Steps:

1. Open the project in Visual Studio.  
2. Open the **Package Manager Console**:  
   `Tools → NuGet Package Manager → Package Manager Console`  
3. Run the following command:

```powershell
Update-Database
