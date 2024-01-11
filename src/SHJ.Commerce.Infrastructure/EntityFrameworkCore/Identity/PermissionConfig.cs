using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SHJ.Commerce.Domain;
using SHJ.Commerce.Domain.Aggregates.Identity;

namespace SHJ.Commerce.Infrastructure.EntityFrameworkCore.Identity;

internal class PermissionConfig : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.HasKey(_ => _.Id);

        builder.Property(_ => _.Name)
               .IsRequired()
               .HasMaxLength(PortalConsts.DefualtMaxLenght);

        builder.Property(_ => _.DisplayName)
               .HasMaxLength(PortalConsts.DefualtMaxLenght);

        builder.Property(_ => _.ParentName)
               .HasMaxLength(PortalConsts.DefualtMaxLenght);

        builder.HasMany(_ => _.Permissions)
               .WithOne()
               .HasForeignKey(_ => _.ParentId)
               .OnDelete(DeleteBehavior.Cascade);



        builder.HasData(
            new Permission(),);
    }
}
