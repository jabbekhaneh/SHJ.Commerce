using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SHJ.Commerce.AuthServer.Web.MVC.Models.Identity.Users;
using SHJ.Commerce.Domain;
using SHJ.Commerce.Domain.Aggregates.Identity;
using SHJ.ExceptionHandler;

namespace SHJ.Commerce.AuthServer.Web.MVC.Controllers;

public class AccountController : Controller
{
    private readonly ILogger<AccountController> _logger;
    private readonly UserManager<User> _userManager;
    public AccountController(ILogger<AccountController> logger, UserManager<User> userManager)
    {
        _logger = logger;
        _userManager = userManager;
    }

    [HttpGet]
    public IActionResult SingUp()
    {
        return View();
    }
    //Aa-123456
    [HttpPost]
    public async Task<IActionResult> SingUp(SignUpViewModel input)
    {
        if (!ModelState.IsValid)
            return View(input);
        var user = await _userManager.FindByEmailAsync(input.Email.ToLower());
        if (user != null)
            throw new BaseBusinessException(GlobalIdentityErrors.DublicationEmail);

        var result = await _userManager.CreateAsync(new User
        {
            Email = input.Email,
            UserName = input.Email,
        }, input.Password);


        if (result.Succeeded) { return Redirect("/"); }

        if (result.Errors.Any())
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }


        return View(input);
    }
}
