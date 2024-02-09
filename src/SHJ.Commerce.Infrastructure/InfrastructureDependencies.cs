using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SHJ.BaseFramework.EntityFrameworkCore;
using SHJ.BaseFramework.Repository;
using SHJ.Commerce.Infrastructure.Common;
using SHJ.Commerce.Infrastructure.EntityFrameworkCore;
using SHJ.Commerce.Shared.Common;

namespace SHJ.Commerce.Infrastructure;

public static class InfrastructureDependencies
{
    public static IServiceCollection UseTokenBase(this IServiceCollection services)
    {

        services.AddAuthentication(options =>
        {
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

        }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
        {
            //If use identity server
            // options.Audience = BaseJwtConsts.IdentityServerUrl;
            //options.SaveToken = true;

        });


        return services;
    }

    public static IServiceCollection RegisterEntityframework(this IServiceCollection services, Action<DbContextOptionsBuilder> options)
    {
        services.AddDbContext<ApplicationDbContext>(options);
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
