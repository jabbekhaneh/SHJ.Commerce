using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Options;
using SHJ.BaseFramework.Shared;
using SHJ.Commerce.Domain.Aggregates.Identity;
using System.Reflection;

namespace SHJ.Commerce.Infrastructure.EntityFrameworkCore;

internal static class ApplicationDbContextConfigurations
{

    public static void BuildApplyConfiguration(this ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
    public static void IndexesConfiguration(this ModelBuilder modelBuilder)
    {
        
    }
    

    public static void QueryFiltersConfiguration(this ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<Page>().HasQueryFilter(it => !it.IsDeleted);
    }

    public static void GenerateDataConfiguration(this ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<Permission>().HasData(
        //    new Permission {  },
        //    new Permission { });

    }
    
    public static void ConfigurationOptionsBuilder(this DbContextOptionsBuilder optionsBuilder,
                                                   IOptions<BaseOptions> Options)
    {
        if (Options.Value.DatabaseType == DatabaseType.InMemory)
        {
            optionsBuilder.UseInMemoryDatabase("App.Db");
            optionsBuilder.ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));
        }
        else if (Options.Value.DatabaseType == DatabaseType.MsSql)
        {
            string connectionString = Options.SetConnectionString();
            optionsBuilder.UseSqlServer(connectionString);
            
        }
        else if (Options.Value.DatabaseType == DatabaseType.Manual)
        {
            optionsBuilder.UseSqlServer(Options.Value.ManualConnectionString);

        }

    }

    public static string SetConnectionString(this IOptions<BaseOptions> Options) 
        => $@"Data Source={Options.Value.SqlOptions.DataSource};Initial Catalog={Options.Value.SqlOptions.DatabaseName};Persist Security Info=True;MultipleActiveResultSets=True;User ID={Options.Value.SqlOptions.UserID};Password={Options.Value.SqlOptions.Password}";
    
}
