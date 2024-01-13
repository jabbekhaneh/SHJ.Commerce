﻿using Microsoft.AspNetCore.Identity;
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
    public async Task Initialize()
    {
        using (var serviceScope = _scopeFactory.CreateScope())
        {
            using (var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>())
            {
                await context.Database.MigrateAsync();
            }
        }
    }

    public async Task SeedData()
    {
        using (var serviceScope = _scopeFactory.CreateScope())
        {
            using (var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>())
            {
                await InitializeUserAdmin(context);

            }
        }
    }

    private static async Task InitializeUserAdmin(ApplicationDbContext? context)
    {
        if (!await context.Users.AnyAsync(_=>_.UserName == UserAdminInfo.AdminUserNameDefaultValue))
        {
            var userAdmin = new User
            {
                FirstName = UserAdminInfo.AdminFirstName,
                LastName = UserAdminInfo.AdminLastName,
                Email = UserAdminInfo.AdminEmailDefaultValue,
                UserName = UserAdminInfo.AdminUserNameDefaultValue,
            };

            var password = new PasswordHasher<User>();
            var passwordHash = password.HashPassword(userAdmin, UserAdminInfo.AdminPasswordDefaultValue);
            userAdmin.PasswordHash = passwordHash;

            var userStore = new UserStore<User, Role, ApplicationDbContext, Guid>(context);

            var result = userStore.CreateAsync(userAdmin);
        }

    }
}
