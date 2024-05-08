using System.ComponentModel.DataAnnotations;

namespace SHJ.Commerce.AuthServer.Web.MVC.Models.Identity.Users;

public class SignUpViewModel
{
    [Required]
    public string Email { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;
    [Compare("Password")]
    public string ConfimedPassword { get; set; } = string.Empty;
}
