using Microsoft.Extensions.Options;
using SHJ.BaseFramework.Shared;

namespace SHJ.Commerce.Domain.Test.Configurations;

public class EntityFrameworkCoreFactory
{
    public static IOptions<BaseOptions> GetOption() => Options.Create(new BaseOptions
    {
        DatabaseType = DatabaseType.InMemory,
    });

}