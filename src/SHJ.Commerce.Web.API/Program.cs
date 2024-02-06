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
    Log.Fatal(ex, "Host terminated unexpectedly! : " + ex.Message);
    if (ex is HostAbortedException)
    {

        throw ex ;
    }



}


public partial class Program { }