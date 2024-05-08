using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SHJ.Commerce.Domain.Aggregates.Identity;
using SHJ.Commerce.Infrastructure;
using SHJ.Commerce.Infrastructure.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connection = "Server=.;Database=dbSoft;Trusted_Connection=True;TrustServerCertificate=True";
// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContextPool<ApplicationDbContext>(options =>
{
    options.UseSqlServer(connection);
});
builder.Services.AddIdentity<User, Role>(options =>
{
    //options.Password.RequireDigit = true;
    //options.Password.RequireLowercase = true;
    //options.Password.RequireNonAlphanumeric = true;
    //options.Password.RequireUppercase = true;
    //options.Password.RequiredLength = 6;
    //options.Password.RequiredUniqueChars = 1;
    //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(42000);
    //options.Lockout.MaxFailedAccessAttempts = 10;
    //options.Lockout.AllowedForNewUsers = true;
    //options.User.AllowedUserNameCharacters =
    //    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    //options.User.RequireUniqueEmail = true;
    //options.SignIn.RequireConfirmedEmail = true;


}).AddEntityFrameworkStores<ApplicationDbContext>()
         .AddDefaultTokenProviders();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
