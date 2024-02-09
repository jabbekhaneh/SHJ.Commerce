namespace SHJ.Commerce.Web.API;

internal static class InternalExtentions
{

    
    public static IApplicationBuilder UseBaseCorsConfig(this IApplicationBuilder app)
    {
        app.UseCors("EnableCorse");
        return app;
    }
    public static IServiceCollection RegisterBaseCorsConfig(this IServiceCollection services)
    {
        services.AddCors(option => option.AddPolicy("EnableCorse", builder =>
        {
            builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().Build();
        }));
        return services;
    }

    public static string DefualtConnectionString = "data source =.; initial catalog =dbCommerce; integrated security = True; MultipleActiveResultSets=True";

    public static string ProductionConnectionString = "Data Source=MsSqLServer2019;Initial Catalog=dbCommerce;Persist Security Info=True;MultipleActiveResultSets=True;User ID=sa;Password=Aa@123456";
}
