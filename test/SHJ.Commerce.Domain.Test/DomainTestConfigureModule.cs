using Autofac;
using SHJ.BaseFramework.DependencyInjection.Modules;
using SHJ.Commerce.Domain.Test.Configurations.Modules;

namespace SHJ.Commerce.Domain.Test;
public class DomainTestConfigureModule : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterModule<AutofacModule>();
        builder.RegisterModule<EntityFrameworkCoreModule>();

        //builder.RegisterType<PageManager>().As<PageManager>().InstancePerLifetimeScope();

    }
}
