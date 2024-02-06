using Autofac.Builder;
using Autofac.Extensions.DependencyInjection;
using Autofac;
using Microsoft.Extensions.DependencyInjection;
using SHJ.Commerce.Infrastructure.EntityFrameworkCore;

namespace SHJ.Commerce.Domain.Test.Configurations;

public abstract class ConfigurationsServiceProvider
{
    protected ApplicationDbContext DatabaseInMemory;
    protected IServiceProvider RootServiceProvider { get; private set; } = GetServiceProvider();

    public ConfigurationsServiceProvider()
    {
        DatabaseInMemory = GetRequiredService<ApplicationDbContext>();
        DatabaseInMemory.Database.EnsureCreated();
    }

    protected virtual T? GetService<T>()
    {
        return RootServiceProvider.GetService<T>();
    }

    protected virtual T GetRequiredService<T>() where T : notnull
    {
        return RootServiceProvider.GetRequiredService<T>();
    }

    public virtual Task ConfigureAsync(ContainerBuilder containerBuilder)
    {
        return Task.CompletedTask;
    }

    private static IServiceProvider GetServiceProvider()
    {
        var containerBuilder = new ContainerBuilder();
        containerBuilder.RegisterModule<DomainTestConfigureModule>();
        return new AutofacServiceProvider(containerBuilder.Build(ContainerBuildOptions.IgnoreStartableComponents));
    }


}
