using Microsoft.Extensions.DependencyInjection;
using SHJ.Commerce.Domain.Test.Configurations;
using SHJ.Commerce.Shared.Common;

namespace SHJ.Commerce.Domain.Test;
public abstract class DomainTestBase : ConfigurationsServiceProvider
{

    public void Initialize()
    {
        using (var scope = RootServiceProvider.CreateScope())
        {
            var dbInitializer = scope.ServiceProvider.GetService<ISeadData>();
            dbInitializer.Initialize();
            dbInitializer.SeedData();
        }
    }


}
