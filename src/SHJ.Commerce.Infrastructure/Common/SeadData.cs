using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SHJ.Commerce.Infrastructure.EntityFrameworkCore;
using SHJ.Commerce.Shared.Common;

namespace SHJ.Commerce.Infrastructure.Common;

public class SeadData  : ISeadData
{
    private readonly IServiceScopeFactory _scopeFactory;
    public SeadData(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }
    public void Initialize()
    {
        using (var serviceScope = _scopeFactory.CreateScope())
        {
            using (var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>())
            {
                context.Database.Migrate();
            }
        }
    }

    public void SeedData()
    {
        using (var serviceScope = _scopeFactory.CreateScope())
        {
            using (var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>())
            {

            }
        }
    }
}
