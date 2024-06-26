﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SHJ.Commerce.Domain.Aggregates.Identity;

namespace SHJ.Commerce.Infrastructure.EntityFrameworkCore;

public class ApplicationDbContext : IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLoginHistory, RoleClaim, UserToken>
{

    #region Const
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }
    //private IBaseClaimService ClaimService;

    //public ApplicationDbContext(DbContextOptions options, IBaseClaimService claimService) : base(options)
    //{
    //    ClaimService = claimService;
    //}

    #endregion



    #region Entities Identity
    public virtual DbSet<Permission> Permissions { get; set; }
    public virtual DbSet<UserAddress> UserAddress { get; set; }
    #endregion

    #region Dynamic

    #endregion

    #region Cms

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

    //public new async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    //{
    //    if (ClaimService.IsAuthenticated())
    //    {
    //        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
    //            switch (entry.State)
    //            {
    //                case EntityState.Added:
    //                    entry.Entity.CreateOn(ClaimService.GetUserId());
    //                    break;
    //                case EntityState.Modified:
    //                    entry.Entity.UpdateOn(ClaimService.GetUserId());
    //                    break;
    //            }
    //    }

    //    return await base.SaveChangesAsync(cancellationToken);
    //}

    #endregion

}
