using Autofac;
using Autofac.Extensions.DependencyInjection;
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
        builder.Services.BuildApplication(builder.Configuration);
        builder.Services.RegisterSwagger(options =>
        {
            options.ProjectName = "*** SHJ Commerce API ***";
        });

        builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
                   .ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new AutofacModule()));

        return builder;
    }
    //##################  Application Builder  ####################
    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.Services.InitializeDatabase();

        app.UseApplication();

        if (app.Environment.IsDevelopment())
        {
            app.RegisterUseSwaggerAndUI();
        }


        app.MapControllers();
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
