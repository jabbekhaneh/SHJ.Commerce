global using Mapster;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SHJ.BaseFramework.AspNet;
using SHJ.Commerce.Infrastructure;
using System.Diagnostics.CodeAnalysis;

namespace SHJ.Commerce.Application;

public static class ApplicationDependencies
{
    public static IServiceCollection BuildApplication([NotNull] this IServiceCollection services)
    {
        services.BuildInfrastructure();
        services.AddSHJBaseFrameworkAspNet(option => { });
        
        return services;
    }

    public static IApplicationBuilder UseApplication([NotNull] this IApplicationBuilder app)
    {
        app.UseSHJBaseFrameworkAspNet();
        
        return app;
    }

   
}
