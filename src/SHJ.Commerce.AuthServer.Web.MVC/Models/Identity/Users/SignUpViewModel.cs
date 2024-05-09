using System.ComponentModel.DataAnnotations;

namespace SHJ.Commerce.AuthServer.Web.MVC.Models.Identity.Users;

public class SignUpViewModel
{
    [Required, MaxLength(255), EmailAddress]
    public string Email { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;

    [Required,Compare("Password")]
    public string ConfimedPassword { get; set; } = string.Empty;
}
