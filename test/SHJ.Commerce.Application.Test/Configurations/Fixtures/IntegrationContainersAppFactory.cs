﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using SHJ.Commerce.Application.Test.Configurations.Fakes;
using SHJ.BaseFramework.Shared;
using SHJ.Commerce.Infrastructure;
using Microsoft.EntityFrameworkCore;

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

            services.RegisterEntityframework(options =>
            {
                options.UseSqlServer(SqlContainerFixture.GetConnectionString, builder =>
                {
                    //var minutes = (int)TimeSpan.FromMinutes(3).TotalSeconds;
                    //builder.CommandTimeout(minutes);
                    //builder.EnableRetryOnFailure();
                });
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
