using Microsoft.Extensions.DependencyInjection;
using SHJ.BaseFramework.EntityFrameworkCore;
using SHJ.BaseFramework.Repository;
using SHJ.Commerce.Infrastructure.Common;
using SHJ.Commerce.Infrastructure.EntityFrameworkCore;
using SHJ.Commerce.Shared.Common;

namespace SHJ.Commerce.Infrastructure;

public static class InfrastructureDependencies
{
    public static IServiceCollection BuildInfrastructure(this IServiceCollection services)
    {
        services.RegisterPages();

        services.RegisterEntityframework();

        services.RegisterIdentity();

        return services;
    }

    public static IServiceProvider InitializeDatabase(this IServiceProvider serviceProvider)
    {
        var scopeFactory = serviceProvider.GetRequiredService<IServiceScopeFactory>();

        using (var scope = scopeFactory.CreateScope())
        {
            var dbInitializer = scope.ServiceProvider.GetService<ISeadData>();
            dbInitializer.Initialize();
            dbInitializer.SeedData();
        }
        return serviceProvider;
    }


    //--------------------------------------------------

    private static IServiceCollection RegisterEntityframework(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>();
        services.AddTransient<IBaseCommandUnitOfWork, BaseEFUnitOfWork<ApplicationDbContext>>();
        services.AddScoped<ISeadData, SeadData>();
        return services;
    }

    private static IServiceCollection RegisterPages(this IServiceCollection services)
    {
        
        return services;
    }
}
