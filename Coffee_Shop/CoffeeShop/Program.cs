using CoffeeShop.Models;
using Microsoft.EntityFrameworkCore;
using System;
using CoffeeShop.Repository;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("JazzCoffee");

//Dependency Injection
builder.Services.AddDbContext<OderDrinkingContext>(options => options.UseSqlServer(connectionString));

//DI
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IGenreRepository, GenreRepository>();
builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();   

app.UseAuthorization();

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

app.Run();
