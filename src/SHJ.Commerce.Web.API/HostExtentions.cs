using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using SHJ.BaseFramework.AspNet;
using SHJ.BaseFramework.DependencyInjection.Modules;
using SHJ.BaseSwagger;
using SHJ.BaseSwagger.Options;
using SHJ.Commerce.Infrastructure;

namespace SHJ.Commerce.Web.API;

public static class HostExtentions
{
    //##################  Application Services  ####################
    public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddSHJBaseFrameworkAspNet(option => { });
        builder.Services.RegisterIdentity();
        builder.Services.UseTokenBase();
        
        if (builder.Environment.IsProduction())
        {
            builder.Services.RegisterEntityframework(options =>
            {
                options.UseSqlServer(InternalExtentions.ProductionConnectionString);
            });
        }

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.RegisterSwagger(options =>
        {
            options.DocumentName = "*** SHJ Commerce API ***";
            options.Authorize = new BaseSwaggerSecurityDefinition
            {
                Key = "Bearer",
                SecurityScheme = new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" },
                    
                },
                
            };

        });
        builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
                   .ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new AutofacModule()));

        return builder;
    }
    //##################  Application Builder  ####################
    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.UseSHJBaseFrameworkAspNet();        

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

        app.UseAuthorization();
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
