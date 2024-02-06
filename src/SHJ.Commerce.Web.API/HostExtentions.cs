using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using SHJ.BaseFramework.DependencyInjection.Modules;
using SHJ.BaseSwagger;
using SHJ.Commerce.Application;
using SHJ.Commerce.Infrastructure;
namespace SHJ.Commerce.Web.API;

public static class HostExtentions
{
    //##################  Application Services  ####################
    public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
    {


        builder.Services.BuildApplication();

        if (builder.Environment.IsProduction())
        {
            builder.Services.RegisterEntityframework(options =>
            {
                options.UseSqlServer(InternalExtentions.ProductionConnectionString);
            });
        }


        builder.Services.RegisterBaseMvcApplication();

        builder.Services.RegisterBaseCorsConfig();

        builder.Services.RegisterSwagger(options =>
        {
            options.ProjectName = "*** SHJ Commerce API ***";
        });

        var sqlOption = builder.Configuration.GetValueBaseSqlOptions();

        builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
                   .ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new AutofacModule()));


        return builder;
    }
    //##################  Application Builder  ####################
    public static WebApplication ConfigurePipeline(this WebApplication app)
    {

        app.UseApplication();

        app.UseHttpsRedirection();
        //app.UseBaseCorsConfig();
        //app.UseSHJExceptionHandler();


        if (app.Environment.IsDevelopment())
        {
            app.Services.InitializeDatabase();
            app.RegisterUseSwaggerAndUI();
        }

        if (app.Environment.IsProduction())
        {
            app.Services.InitializeDatabase();
            app.RegisterUseSwaggerAndUI();
        }


        app.MapControllers();

        //app.UseRouting();

        //app.MapControllerRoute(name: "default",
        //                       pattern: "{controller=Home}/{action=Index}/{id?}");
        return app;
    }

    //##################  Host  Logger  ####################
    public static WebApplicationBuilder ConfigureHostLogger(this WebApplicationBuilder builder)
    {
        
        Log.Logger = new LoggerConfiguration()
        .MinimumLevel.Information()
        .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
        .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
        .Enrich.FromLogContext()
        .WriteTo.File("Logs/logs.txt")
        .WriteTo.Console()
        .CreateLogger();
        Log.Information("Starting SHJ.Commerce.Web.API");
        builder.Host.UseSerilog();
        return builder;
    }
}
