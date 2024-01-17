using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using SHJ.Commerce.Application.Test.Configurations.Fakes;
using SHJ.BaseFramework.AspNet;
using SHJ.BaseFramework.Shared;

namespace SHJ.Commerce.Application.Test.Configurations.Fixtures;

public class IntegrationContainersAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    public MssqlContainerFixture SqlContainerFixture { get; }
    public IntegrationContainersAppFactory()
    {
        SqlContainerFixture = new MssqlContainerFixture();

    }
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);

        builder.ConfigureAppConfiguration(app =>
        {

        });

        builder.ConfigureTestServices(services =>
        {
            var serviceProvider = services.BuildServiceProvider();
            services.AddSHJBaseFrameworkAspNet(option =>
            {
                option.DatabaseType = DatabaseType.Manual;

                option.Environment = ASPNET_EnvironmentType.Development;
                option.SqlOptions = new BaseSqlServerOptions
                {
                    DataSource=SqlContainerFixture.DataSource,
                    DatabaseName=SqlContainerFixture.DatabaseName,
                    UserID= SqlContainerFixture.UserID,
                    Password = SqlContainerFixture.Password,
                };
                option.ManualConnectionString = SqlContainerFixture.GetConnectionString();

            });
            services.AddScoped<IBaseClaimService, FakeClaimService>();
        });
    }

    public async Task InitializeAsync()
    {
        await SqlContainerFixture.Container.StartAsync();
    }

    public async Task DisposeAsync()
    {
        await SqlContainerFixture.Container.StopAsync();
    }



}
