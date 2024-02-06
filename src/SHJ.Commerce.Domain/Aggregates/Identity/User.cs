using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SHJ.Commerce.Domain.Aggregates.Identity;

public class User : IdentityUser<Guid> 
{
    public User()
    {
        
        UserAddress = new List<UserAddress>();
    }
    [MaxLength(256)]
    public virtual string? FirstName { get; set; } = string.Empty;

    [MaxLength(256)]
    public virtual string? LastName { get; set; } = string.Empty;

    [MaxLength(256)]
    public virtual string? Job { get; set; } = string.Empty;

    public virtual int? Age { get; set; }

    [MaxLength(25)]
    public virtual string? Mobile { get; set; } = string.Empty;
    public virtual bool? MobileNumberConfirmed { get; set; }

    public virtual decimal? Wallet { get; set; } = 0;

    public virtual DateTime? DateOfBirth { get; set; }

    [MaxLength(256)]
    public virtual string? CompanyName { get; set; } = string.Empty;

    public virtual string? Avatar { get; set; } = string.Empty;

    public virtual List<UserAddress> UserAddress { get; set; } 
}
