using SHJ.BaseFramework.Shared;
using System.ComponentModel.DataAnnotations;

namespace SHJ.Commerce.ApplicationContracts.Contracts.Identity;

public class SignUpDto : BaseDto
{
    [Required, MaxLength(255), EmailAddress]
    public string Email { get; set; } = string.Empty;

    [MaxLength(255)]
    public string? FirstName { get; set; } = string.Empty;

    [MaxLength(255)]
    public string? LastName { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;

}