using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Options;
using SHJ.BaseFramework.Domain;
using SHJ.BaseFramework.Shared;
using SHJ.Commerce.Domain.Aggregates.Dynamic;
using SHJ.Commerce.Domain.Aggregates.Identity;
using System.Reflection;

namespace SHJ.Commerce.Infrastructure.EntityFrameworkCore;

public class ApplicationDbContext : IdentityDbContext<User, Role, Guid>
{
    #region Entities
    
    #endregion


    #region Const
    private IOptions<BaseOptions> Options;
    private IBaseClaimService ClaimService;
    public ApplicationDbContext() { }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IBaseClaimService claimService, IOptions<BaseOptions> baseOptions) : base(options)
    {
        Options = baseOptions;
        ClaimService = claimService;
    }
    #endregion


    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (Options.Value.DatabaseType == DatabaseType.InMemory)
        {
            optionsBuilder.UseInMemoryDatabase("App.Db");
            optionsBuilder.ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));
        }
        else if (Options.Value.DatabaseType == DatabaseType.MsSql)
        {
            string connectionString = SetConnectionString();
            optionsBuilder.UseSqlServer(connectionString);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(connectionString);
        }
        else if (Options.Value.DatabaseType == DatabaseType.Manual)
        {
            optionsBuilder.UseSqlServer(Options.Value.ManualConnectionString);
            Console.ForegroundColor = ConsoleColor.Yellow;

        }

        base.OnConfiguring(optionsBuilder);
    }
    private string SetConnectionString()
    {
        return $@"Data Source={Options.Value.SqlOptions.DataSource};Initial Catalog={Options.Value.SqlOptions.DatabaseName};Persist Security Info=True;MultipleActiveResultSets=True;User ID={Options.Value.SqlOptions.UserID};Password={Options.Value.SqlOptions.Password}";
    }

    public new async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        if (ClaimService.IsAuthenticated())
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreateOn(ClaimService.GetUserId());
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdateOn(ClaimService.GetUserId());
                        break;
                }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }

}
