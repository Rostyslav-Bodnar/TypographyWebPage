using Microsoft.EntityFrameworkCore;
using Курсова_робота.Models.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Курсова_робота.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DB>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IMenuService, MenuService>();

// Add services to the container.
builder.Services.AddControllersWithViews();
// В Startup.cs або Program.cs залежно від версії ASP.NET Core
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<OperationsFilter>();
});

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Час життя сесії
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<CartService>();


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Auth/SignIn"; // Шлях до сторінки входу
        options.LogoutPath = "/Auth/SignOut"; // Шлях до виходу
        options.AccessDeniedPath = "/Auth/AccessDenied"; // Шлях для відмови у доступі
    });

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession(); // Додайте сесію
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints => { _ = endpoints.MapControllers(); });

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
