using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SHJ.BaseFramework.AspNet;
using SHJ.BaseFramework.Shared;
using SHJ.Commerce.Infrastructure;
using SHJ.ExceptionHandler;
using System.Diagnostics.CodeAnalysis;
namespace SHJ.Commerce.Application;

public static class ApplicationDependencies
{

    public static IServiceCollection BuildApplication([NotNull] this IServiceCollection services, IConfiguration configuration)
    {

        services.BuildInfrastructure(); ;
        
        services.AddSHJExceptionHandler(option => { });

        var sqlOption = configuration.GetValueBaseSqlOptions();


        services.AddSHJBaseFrameworkAspNet(option =>
        {
            option.DatabaseType = DatabaseType.MsSql;
            option.Environment = ASPNET_EnvironmentType.Development;
            option.SqlOptions = new BaseSqlServerOptions
            {
                ConnectToServer = DatabaseConnectType.SqlServerAuthentication,
                DatabaseName = sqlOption.DatabaseName,
                DataSource = sqlOption.DataSource,
                UserID = sqlOption.UserID,
                Password = sqlOption.Password,
            };
        });

        services.AddBaseMvcApplication();
        services.AddBaseCorsConfig();

        return services;
    }

    public static IApplicationBuilder UseApplication([NotNull] this IApplicationBuilder app)
    {
        app.UseSHJBaseFrameworkAspNet();
        app.UseSHJExceptionHandler();

        app.UseHttpsRedirection();
        app.UseBaseCorsConfig();

        return app;
    }

    #region CorsConfig
    private static IApplicationBuilder UseBaseCorsConfig([NotNull] this IApplicationBuilder app)
    {
        app.UseCors("EnableCorse");
        return app;
    }
    private static IServiceCollection AddBaseCorsConfig([NotNull] this IServiceCollection services)
    {
        services.AddCors(option => option.AddPolicy("EnableCorse", builder =>
        {
            builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().Build();
        }));
        return services;
    }

    #endregion

    #region Private Methods

    private static IServiceCollection AddBaseMvcApplication(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        return services;
    }

    #endregion
}
