using BilgeShop.Business2.Manager;
using BilgeShop.Business2.Services;
using BilgeShop.Data2.Context;
using BilgeShop.Data2.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<BilgeShopContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddScoped(typeof(IRepository<>), typeof(SqlRepository<>));
builder.Services.AddScoped<IUserService, UserManager>();
builder.Services.AddScoped<ICategoryService, CategoryManager>();
builder.Services.AddScoped<IProductService, ProductManager>();

builder.Services.AddDataProtection();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.LoginPath = new PathString("/");
    options.LogoutPath = new PathString("/");
    options.AccessDeniedPath = new PathString("/");
});


var app = builder.Build();

app.UseStaticFiles(); //root kullan�lacak demektir.


app.UseAuthentication(); // Login-Logout i�lemleri i�in ve Claim
app.UseAuthorization(); // Yaln�zca admin y�netim paneline gidebilsin.

app.MapControllerRoute(
    name: "areas",
    pattern: ("{area:exists}/{controller=Dashboard}/{action=Index}/{id?}")
    );

app.MapControllerRoute(
    name: "default",
    pattern: ("{controller=home}/{action=index}/{id?}")
    );

app.Run();
