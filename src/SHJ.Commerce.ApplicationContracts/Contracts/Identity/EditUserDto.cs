using SHJ.BaseFramework.Shared;
using System.ComponentModel.DataAnnotations;

namespace SHJ.Commerce.ApplicationContracts.Contracts.Identity;

public class EditUserDto : BaseDto
{
    public EditUserDto()
    {
        RoleNames = new List<string>();
    }
    
    [MaxLength(256)]
    public  string? FirstName { get; set; } = string.Empty;

    [MaxLength(256)]
    public  string? LastName { get; set; } = string.Empty;

    [MaxLength(256)]
    public  string? Job { get; set; } = string.Empty;

    public  int? Age { get; set; }

    [MaxLength(25)]
    public  string? Mobile { get; set; } = string.Empty;
    public  bool? MobileNumberConfirmed { get; set; }

    public  decimal? Wallet { get; set; } = 0;

    public  DateTime? DateOfBirth { get; set; }

    [MaxLength(256)]
    public  string? CompanyName { get; set; } = string.Empty;

    public  string? Avatar { get; set; } = string.Empty;
    public List<string> RoleNames { get; set; }
}