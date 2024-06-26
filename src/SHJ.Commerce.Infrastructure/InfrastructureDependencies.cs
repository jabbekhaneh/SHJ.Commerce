﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SHJ.BaseFramework.EntityFrameworkCore;
using SHJ.BaseFramework.Repository;
using SHJ.Commerce.Infrastructure.Common;
using SHJ.Commerce.Infrastructure.EntityFrameworkCore;
using SHJ.Commerce.Shared.Common;
using System.Text;

namespace SHJ.Commerce.Infrastructure;

public static class InfrastructureDependencies
{
    public static IServiceCollection UseTokenBase(this IServiceCollection services)
    {

        
        services.AddAuthentication(options =>
        {
            options.DefaultChallengeScheme = BaseJwtConsts.DefaultScheme;
            options.DefaultAuthenticateScheme = BaseJwtConsts.DefaultScheme;

        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ClockSkew = TimeSpan.Zero,
                ValidateLifetime = true,
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.ASCII.GetBytes(BaseJwtConsts.SecurityKey)),
            };


        });


        return services;
    }

    public static IServiceCollection RegisterEntityframework(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>();
        services.AddTransient<IBaseCommandUnitOfWork, BaseEFUnitOfWork<ApplicationDbContext>>();
        services.AddTransient<ISeadData, SeadData>();
        return services;
    }

    public static IServiceProvider InitializeDatabase(this IServiceProvider serviceProvider)
    {
        var scopeFactory = serviceProvider.GetRequiredService<IServiceScopeFactory>();

        using (var scope = scopeFactory.CreateScope())
        {
            var dbInitializer = scope.ServiceProvider.GetService<ISeadData>();
            dbInitializer?.AutomatedMigration();

            dbInitializer?.Initialize();
        }
        return serviceProvider;
    }

}
