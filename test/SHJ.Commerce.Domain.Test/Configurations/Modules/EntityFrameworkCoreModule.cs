using Autofac;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SHJ.BaseFramework.Shared;
using SHJ.Commerce.Domain.Test.Configurations.Fakes;
using SHJ.Commerce.Infrastructure.EntityFrameworkCore;

namespace SHJ.Commerce.Domain.Test.Configurations.Modules;
public class EntityFrameworkCoreModule : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>().SingleInstance();
        builder.RegisterType<ClaimServiceFake>().As<IBaseClaimService>().InstancePerDependency();

        builder.RegisterType<ApplicationDbContext>()
       .InstancePerLifetimeScope()
       .WithParameter("options", new DbContextOptions<ApplicationDbContext>())
       .WithParameter("baseOptions", EntityFrameworkCoreFactory.GetOption());
    }
}
