using Serilog;
using SHJ.Commerce.Web.API;

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.ConfigureServices()
           .ConfigureHostLogger().Build()
           .ConfigurePipeline().Run();

}
catch (Exception ex)
{
    if (ex is HostAbortedException)
    {
        throw;
    }

    Log.Fatal(ex, "Host terminated unexpectedly!");

}


public partial class Program { }