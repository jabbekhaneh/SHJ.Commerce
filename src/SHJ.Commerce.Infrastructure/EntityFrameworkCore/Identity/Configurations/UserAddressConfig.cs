using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SHJ.Commerce.Domain.Aggregates.Identity;

namespace SHJ.Commerce.Infrastructure.EntityFrameworkCore.Identity.Configurations;

internal class UserAddressConfig : IEntityTypeConfiguration<UserAddress>
{
    public void Configure(EntityTypeBuilder<UserAddress> builder)
    {
        builder.HasKey(_ => _.Id);

        builder.OwnsOne(_ => _.Address);

        builder.HasOne(_ => _.User)
               .WithMany(_ => _.UserAddress)
               .HasForeignKey(_ => _.UserId)
               .OnDelete(DeleteBehavior.Restrict);


        builder.OwnsOne(_ => _.Address)
            .Property(_ => _.Title).HasColumnName("Title");

        builder.OwnsOne(_ => _.Address)
            .Property(_ => _.Country).HasColumnName("Country");

        builder.OwnsOne(_ => _.Address)
            .Property(_ => _.Province).HasColumnName("Province");

        builder.OwnsOne(_ => _.Address)
            .Property(_ => _.City).HasColumnName("City");

        builder.OwnsOne(_ => _.Address)
            .Property(_ => _.Street).HasColumnName("Street");

        builder.OwnsOne(_ => _.Address)
            .Property(_ => _.ZipCode).HasColumnName("ZipCode");

        builder.OwnsOne(_ => _.Address)
            .Property(_ => _.Latitude).HasColumnName("Latitude");

        builder.OwnsOne(_ => _.Address)
            .Property(_ => _.Longitude).HasColumnName("Longitude");



    }
}
