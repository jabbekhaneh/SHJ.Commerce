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
    private readonly SignInManager<User> _signInManager;
    public AccountController(ILogger<AccountController> logger, UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _logger = logger;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpGet]
    public IActionResult SingUp()
    {
        return View();
    }

    [HttpPost, ValidateAntiForgeryToken]
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

    [HttpGet]
    public IActionResult SingIn(string? returnUrl)
    {
        if (_signInManager.IsSignedIn(User))
            return Redirect("/");
        ViewData["returnUrl"] = returnUrl;
        return View();
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> SingIn(SignInViewModel input, string? ReturnUrl = null)
    {
        if (!ModelState.IsValid)
            return View(input);

        var result = await _signInManager
            .PasswordSignInAsync(input.UserName, input.Password, input.IsPersistent, true);

        if (result.Succeeded)
        {
            //if (!returnUrl.IsNullOrEmpty() && Url.IsLocalUrl(returnUrl))
            if (!ReturnUrl.IsNullOrEmpty())
                return Redirect(ReturnUrl);

            return Redirect("/");
        }

        if (result.IsLockedOut)
        {
            ModelState.AddModelError("", " حساب کاربری قفل شده 20 دقیقه دیگر مراجعه کنید ");
            return View(input);
        }
        ModelState.AddModelError("", "نام کاربری یا کلمه عبور صحیحی نمی باشد");


        return View(input);
    }


    [HttpGet]
    public async Task<IActionResult> SingOut()
    {
        await _signInManager.SignOutAsync();
        return Redirect("/");

    }
}
