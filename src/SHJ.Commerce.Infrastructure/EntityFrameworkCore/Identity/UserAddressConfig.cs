using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SHJ.Commerce.Domain.Aggregates.Identity;

namespace SHJ.Commerce.Infrastructure.EntityFrameworkCore.Identity;

internal class UserAddressConfig : IEntityTypeConfiguration<UserAddress>
{
    public void Configure(EntityTypeBuilder<UserAddress> builder)
    {
        throw new NotImplementedException();
    }
}
