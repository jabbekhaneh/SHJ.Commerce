using SHJ.BaseFramework.Domain;
using SHJ.Commerce.Domain.Common.ValueObjects;

namespace SHJ.Commerce.Domain.Aggregates.Identity;

public class UserAddress : BaseEntity<Guid>
{
    public UserAddress(Address address, Guid userId)
    {
        Address = address;
        UserId = userId;
    }

    public Address Address { get; set; }
    public Guid UserId { get; set; }
}
