using System.ComponentModel.DataAnnotations;

namespace SHJ.Commerce.AuthServer.Web.MVC.Models.Identity.Users;

public class SignInViewModel
{
    [Required]
    public string UserName { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;

    public bool IsPersistent { get; set; } = false;
}
