using CoffeeShop.Models;
using Microsoft.EntityFrameworkCore;
using System;
using CoffeeShop.Repository;
using Microsoft.AspNetCore.Identity;
using CoffeeShop.Data;
using CoffeeShop.Areas.Identity.Data;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("JazzCoffee");

//Dependency Injection
builder.Services.AddDbContext<OderDrinkingContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDbContext<CoffeeShopDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CoffeeShopDBContext"));

});
builder.Services.ConfigureApplicationCookie(options =>
{
    // Cookie settings
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

    options.LoginPath = "/Identity/Account/LoginCustomer";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.SlidingExpiration = true;
});

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<CoffeeShopDBContext>();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
});


//DI
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IGenreRepository, GenreRepository>();
builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();
builder.Services.AddTransient<IBillRepository, BillRepository>();
builder.Services.AddTransient<IBillDetailRepository, BillDetailRepository>();
builder.Services.AddSession();

// Add services to the container.
builder.Services.AddControllersWithViews()
               .AddRazorPagesOptions(
         options=>options.Conventions.AddAreaPageRoute("Identity", "/Account/LoginCustomer", "/Account/Login/LoginCustomer")); 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();   
app.UseAuthentication();

app.UseAuthorization();

app.UseSession();

/*app.MapAreaControllerRoute(
    name: "MyAreas",
    areaName: "Admin",
    pattern: "Admin/{controller=Dashboard}/{action=Login}/{id?}"
    );*/

//app.MapAreaControllerRoute(
//    name: "MyAreas",
//    areaName: "Admin",
//    pattern: "Admin/{action=Products}/{id?}",
//    defaults: new { controller = "Home", action = "Products" }
//    );

//app.MapAreaControllerRoute(
//    name: "MyAreas",
//    areaName: "Admin",
//    pattern: "Admin/{action=Index}/{id?}",
//    defaults: new { controller = "Genre", action = "Index" }
//    );

app.MapAreaControllerRoute(
    name: "MyAreas",
    areaName: "Admin",
    pattern: "Admin/{controller}/{action}/{id?}",
    defaults: new { controller = "Home", action = "Products" }
    );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
app.Run();
