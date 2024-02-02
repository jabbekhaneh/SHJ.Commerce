using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SHJ.Commerce.Domain.Aggregates.Identity;
using SHJ.Commerce.Infrastructure.EntityFrameworkCore;
using SHJ.Commerce.Shared.Common;

namespace SHJ.Commerce.Infrastructure.Common;

public class SeadData : ISeadData
{
    private readonly IServiceScopeFactory _scopeFactory;
    public SeadData(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    public void Initialize()
    {
        using (var serviceScope = _scopeFactory.CreateScope())
        {
            using (var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>())
            {
                context.Database.Migrate();
            }
        }
    }

    public void SeedData()
    {
        using (var serviceScope = _scopeFactory.CreateScope())
        {
            using (var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>())
            {
                InitializePermissions(context);
                //InitializeUserAdmin(context);
                context.SaveChanges();
            }
        }
    }

    private static void InitializePermissions(ApplicationDbContext? context)
    {
        var permissions = new List<Permission>();
        if (!context.Permissions.Any())
        {
            permissions.Add(new Permission("Manager", "manager"));
            permissions.Add(new Permission("Client", "client"));
        }
        context.Permissions.AddRange(permissions);

    }

    private static void InitializeUserAdmin(ApplicationDbContext? context)
    {
        if (!context.Users.Any(_ => _.UserName == UserAdminInfo.AdminUserNameDefaultValue))
        {
            var userAdmin = new User
            {
                FirstName = UserAdminInfo.AdminFirstName,
                LastName = UserAdminInfo.AdminLastName,
                Email = UserAdminInfo.AdminEmailDefaultValue.ToLower(),
                UserName = UserAdminInfo.AdminUserNameDefaultValue.ToLower(),
                EmailConfirmed=true,
                MobileNumberConfirmed=true,               
                
            };

            var password = new PasswordHasher<User>();
            var passwordHash = password.HashPassword(userAdmin, UserAdminInfo.AdminPasswordDefaultValue);
            userAdmin.PasswordHash = passwordHash;

            var userStore = new UserStore<User, Role, ApplicationDbContext, Guid>(context);

            var result = userStore.CreateAsync(userAdmin);
        }

    }
}
