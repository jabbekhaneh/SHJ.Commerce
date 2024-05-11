namespace SHJ.Commerce.AuthServer.Web.MVC;

public static class HostExtentions
{
    //##################  Application Services  ####################
    public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
    {
        return builder;
    }
    //##################  Application Builder  ####################
    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        return app;
    }

    //##################  Private Methods  ####################

    public static WebApplicationBuilder ConfigurationIdentity(this WebApplicationBuilder builder)
    {
        return builder;
    }
    public static WebApplicationBuilder ConfigurationIdentityServer(this WebApplicationBuilder builder)
    {
        return builder;
    }
}
