using Microsoft.EntityFrameworkCore;
using �������_������.Models.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using �������_������.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DB>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IMenuService, MenuService>();

// Add services to the container.
builder.Services.AddControllersWithViews();
// � Startup.cs ��� Program.cs ������� �� ���� ASP.NET Core
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<OperationsFilter>();
});

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // ��� ����� ���
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<CartService>();


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Auth/SignIn"; // ���� �� ������� �����
        options.LogoutPath = "/Auth/SignOut"; // ���� �� ������
        options.AccessDeniedPath = "/Auth/AccessDenied"; // ���� ��� ������ � ������
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

app.UseSession(); // ������� ����
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints => { _ = endpoints.MapControllers(); });

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
