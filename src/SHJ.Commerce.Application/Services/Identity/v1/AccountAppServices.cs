using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SHJ.BaseFramework.AspNet.Services;
using SHJ.BaseFramework.Shared;
using SHJ.Commerce.ApplicationContracts.Contracts.Identity;
using SHJ.Commerce.Domain;
using SHJ.Commerce.Domain.Aggregates.Identity;
using SHJ.ExceptionHandler;

namespace SHJ.Commerce.Application.Services.Identity.v1;

[BaseControllerName("Account")]
public class AccountAppServices : BaseAppService, IAccountAppServices
{
    private readonly SignInManager<User> _signInManager;
    protected readonly UserManager<User> _userManager;
    public AccountAppServices(SignInManager<User> signInManager, UserManager<User> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    [HttpPost, ActionName("SignIn")]
    public async Task<BaseResult> SignIn(SignIn input)
    {
        if (_signInManager.IsSignedIn(User))
            throw new BaseBusinessException(GlobalIdentityErrors.AccessDenied, "AccessDenied");

        if (!ModelState.IsValid)
            return await FailRequestAsync(ModelState);

        var result = await _signInManager
            .PasswordSignInAsync(input.UserName, input.Password, input.IsPersistent, true);
        
        result.CheckSignInResult();

        return await OkAsync();
    }

    [HttpPost("Signout")]
    public async Task Signout()
    {
        await _signInManager.SignOutAsync();
    }

    [HttpPost("SignUp")]
    public async Task<BaseResult> SignUp(SignUp input)
    {

        if (!ModelState.IsValid)
            return await FailRequestAsync(ModelState);

        var newUser = new User()
        {
            Email = input.Email,
            EmailConfirmed = false,
            FirstName = input.FirstName,
            LastName = input.LastName,
            UserName = input.Email,
        };

        var result = await _userManager.CreateAsync(newUser);
        result.CheckErrors();
        return await ReturnResultAsync(newUser);
    }

}
