﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SHJ.BaseFramework.Domain;
using SHJ.BaseFramework.Shared;
using SHJ.Commerce.Domain.Aggregates.Identity;

namespace SHJ.Commerce.Infrastructure.EntityFrameworkCore;

public class ApplicationDbContext : IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLoginHistory, RoleClaim, UserToken>
{
    #region Entities Identity
    public virtual DbSet<Permission> Permissions { get; set; }
    public virtual DbSet<UserAddress> UserAddress { get; set; }
    #endregion

    #region Dynamic

    #endregion

    #region Cms

    #endregion


    #region Const
    private IOptions<BaseOptions> Options;
    private IBaseClaimService ClaimService;
    public ApplicationDbContext() { }

    public ApplicationDbContext(DbContextOptions options):base(options) { }
    public ApplicationDbContext(DbContextOptions options, IBaseClaimService claimService, IOptions<BaseOptions> baseOptions) : base(options)
    {
        Options = baseOptions;
        ClaimService = claimService;
    }
    #endregion



    #region ContextConfigurations
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.BuildApplyConfiguration();
        builder.IndexesConfiguration();
        builder.GenerateDataConfiguration();
        builder.QueryFiltersConfiguration();

        base.OnModelCreating(builder);
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //AddMigarion(optionsBuilder);
        optionsBuilder.ConfigurationOptionsBuilder(Options);
        base.OnConfiguring(optionsBuilder);
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

    #endregion
    private static void AddMigarion(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("data source =.; initial catalog =dbCommerce; integrated security = True; MultipleActiveResultSets=True");
    }
}
