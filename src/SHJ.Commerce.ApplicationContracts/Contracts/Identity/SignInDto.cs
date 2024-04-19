using SHJ.BaseFramework.Shared;
using System.ComponentModel.DataAnnotations;

namespace SHJ.Commerce.ApplicationContracts.Contracts.Identity;

public class SignInDto : BaseDto
{
    [Required,MaxLength(256)]
    public string UserName { get; set; } = string.Empty;

    [Required,MaxLength(256)]
    public string Password { get; set; } = string.Empty;

    public bool IsPersistent { get; set; } = false;
}