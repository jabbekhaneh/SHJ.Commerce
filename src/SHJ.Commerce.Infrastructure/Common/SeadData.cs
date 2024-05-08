using Microsoft.AspNetCore.Identity;
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

    public async Task Initialize()
    {
        using (var serviceScope = _scopeFactory.CreateScope())
        {
            using (var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>())
            {
                if (!await context.Permissions.AnyAsync())
                {
                    var permissionManager = serviceScope.ServiceProvider.GetRequiredService<PermissionManager>();
                    await permissionManager.InitializePermissionsAsync();
                }

                if (!await context.Roles.AnyAsync())
                {
                    var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
                    await roleManager.InitializeRoleAdminAsync();
                }

                if (!await context.Users.AnyAsync())
                {
                    var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>();
                    await userManager.InitializeUserAdminAsync();
                }


                await context.SaveChangesAsync();
            }
        }

    }

    public void AutomatedMigration()
    {
        using (var serviceScope = _scopeFactory.CreateScope())
        {
            using (var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>())
            {
                if (context.Database.IsSqlServer())
                    context?.Database.Migrate();
            }
        }
    }
}
