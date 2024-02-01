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
    
    public AccountAppServices(SignInManager<User> signInManager)
    {
        _signInManager = signInManager;
        
    }

    [HttpPost]
    public async Task<BaseResult> SignIn([FromBody]SignIn input)
    {
        if (_signInManager.IsSignedIn(User))
            throw new BaseBusinessException(GlobalIdentityErrors.AccessDenied, "AccessDenied");

        if (!ModelState.IsValid)
            return await FailRequestAsync(ModelState);

        var result = await _signInManager
            .PasswordSignInAsync(input.UserName, input.Password, input.IsPersistent, true);
        
        result.CheckSignInResultErrors();

        return await OkAsync();
    }

    [HttpPatch]
    public async Task Signout()
    {
        await _signInManager.SignOutAsync();
    }

   

}
