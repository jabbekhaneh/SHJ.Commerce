using SHJ.BaseFramework.Shared;
using System.ComponentModel.DataAnnotations;

namespace SHJ.Commerce.ApplicationContracts.Contracts.Identity;

public class EditUserDto : BaseDto
{
    public EditUserDto()
    {
        RoleNames = new List<string>();
    }
    [Required]
    public string Email { get; set; } = string.Empty;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    [Required, MinLength(8)]
    public string Password { get; set; } = string.Empty;
    public List<string> RoleNames { get; set; }
}