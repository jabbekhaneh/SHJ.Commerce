using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SHJ.Commerce.Domain.Aggregates.Identity;

public class UserLoginHistory : IdentityUserLogin<Guid>
{
    public UserLoginHistory()
    {

    }
    [MaxLength(60)]
    public virtual string? IP { get; set; } = string.Empty;
    [MaxLength(60)]
    public virtual string? OS { get; set; } = string.Empty;
}
