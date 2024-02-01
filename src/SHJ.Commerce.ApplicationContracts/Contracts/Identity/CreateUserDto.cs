using SHJ.BaseFramework.Shared;
using System.ComponentModel.DataAnnotations;

namespace SHJ.Commerce.ApplicationContracts.Contracts.Identity;

public class CreateUserDto : BaseDto
{
    public CreateUserDto()
    {
        RoleNames = new();
    }

    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    [Required, MinLength(8)]
    public string Password { get; set; } = string.Empty;
    public List<string> RoleNames { get; set; }
}
