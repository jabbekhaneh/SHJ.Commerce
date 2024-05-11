using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SHJ.Commerce.AuthServer.Web.MVC;
using SHJ.Commerce.Domain.Aggregates.Identity;
using SHJ.Commerce.Infrastructure.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connection = "Server=.;Database=dbSoft;Trusted_Connection=True;TrustServerCertificate=True";
// Add services to the container.



//----------------------------------------
builder.Services.AddDbContextPool<ApplicationDbContext>(options =>
{
    options.UseSqlServer(connection);
});
builder.Services.AddIdentity<User, Role>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 0;

    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(20);
    options.Lockout.MaxFailedAccessAttempts = 10;
    options.Lockout.AllowedForNewUsers = true;

    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+#";
    options.User.RequireUniqueEmail = true;

   //options.SignIn.RequireConfirmedEmail = true; 

}).AddEntityFrameworkStores<ApplicationDbContext>()
  .AddDefaultTokenProviders();
builder.Services.ConfigureApplicationCookie(option =>
{
    option.LoginPath = "/Account/SingIn";
    option.LogoutPath = "/Account/SingOut";
    //option.Cookie.Name=""
    //option.ExpireTimeSpan

});
//----------------------------------------
builder.Services.AddIdentityServer()
    .AddDeveloperSigningCredential()
    .AddInMemoryIdentityResources(Configuration.GetIndentityResources())
    .AddInMemoryClients(Configuration.GetClients())
    .AddTestUsers(Configuration.Users);
//----------------------------------------
builder.Services.AddControllersWithViews()
    .AddRazorRuntimeCompilation();

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

app.UseIdentityServer();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
