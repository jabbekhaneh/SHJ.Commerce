using SHJ.BaseFramework.Domain;
using SHJ.Commerce.Domain.Common.ValueObjects;

namespace SHJ.Commerce.Domain.Aggregates.Identity;


public class UserAddress : BaseEntity<Guid>
{
    public UserAddress() { }
    public UserAddress(Address address, Guid userId)
    {
        Address = address;
        UserId = userId;
    }
    public virtual Address? Address { get; set; }
    public virtual Guid UserId { get; set; }
    public virtual User  User { get; set; }
}
