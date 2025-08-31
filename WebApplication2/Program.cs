using MaintenanceServiceMVC.Data;
using MaintenanceServiceMVC.Models;   // make sure ApplicationUser is here
using MaintenanceServiceMVC.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MaintenanceServiceMVC.Repositories;
using NuGet.Protocol.Core.Types;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<Repository<ServiceRequest>>();
builder.Services.AddScoped<Repository<Customer>>();
builder.Services.AddScoped<Repository<Professional>>();
builder.Services.AddScoped<Repository<Service>>();

// If you have a generic interface IRepository<T>, you can also do:
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// Add DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false; // you can set true if you want email confirmation
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

// Register AuthService
builder.Services.AddScoped<AuthService>();

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();


app.UseAuthentication();   // this one was missing
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.MapRazorPages();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    await DbInitializer.SeedRolesAsync(roleManager);
}

using (var scope = app.Services.CreateScope())
{
    await SeedData.SeedAdminAsync(scope.ServiceProvider);
}


app.Run();
