using Microsoft.AspNetCore.Mvc;
using SHJ.Commerce.AuthServer.Web.MVC.Models.Identity.Users;

namespace SHJ.Commerce.AuthServer.Web.MVC.Controllers;

public class AccountController : Controller
{
    private readonly ILogger<AccountController> _logger;

    public AccountController(ILogger<AccountController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Register(SignUpViewModel model)
    {
        if(!ModelState.IsValid) { return View(model); }


        return View();
    }
}
