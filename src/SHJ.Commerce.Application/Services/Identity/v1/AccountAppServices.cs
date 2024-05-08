using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SHJ.BaseFramework.AspNet.Services;
using SHJ.BaseFramework.Shared;
using SHJ.Commerce.ApplicationContracts.Contracts.Identity;
using SHJ.Commerce.Domain;
using SHJ.Commerce.Domain.Aggregates.Identity;
using SHJ.Commerce.Infrastructure;
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

    [HttpPost("SingIn")]
    public async Task<BaseResult> SignIn([FromBody] SignInDto input)
    {
        if (_signInManager.IsSignedIn(User))
            throw new BaseBusinessException(GlobalIdentityErrors.AccessDenied, "AccessDenied");

        if (!ModelState.IsValid)
            return await FailRequestAsync(ModelState);

        var user = await _signInManager.UserManager.FindByNameAsync(input.UserName);
        if (user is null)
            throw new BaseBusinessException(GlobalIdentityErrors.UserNotFound, "UserNotFound");

        var result = await _signInManager.CheckPasswordSignInAsync(user, input.Password, false);
        result.CheckSignInResultErrors();

        if (result.Succeeded)
        {
            var token = _signInManager.GenerateToken(user);
            return await ReturnResultAsync(token);
        }
      
        return await FailRequestAsync();
    }

    [HttpGet, Authorize]
    public async Task<BaseResult<ProfileDto>> Profile()
    {
        var userId = ClaimService.GetUserId().ToString();

        var user = _signInManager.UserManager.FindByIdAsync(userId);
        var profile = user.Adapt<ProfileDto>();

        return await OkAsync<ProfileDto>(profile);
    }

    //[HttpPut]
    //public async Task<BaseResult> Profile([FromBody] ProfileDto input)
    //{
    //    var userId = ClaimService.GetUserId().ToString();
    //    var user = await _signInManager.UserManager.FindByIdAsync(userId);
    //    var editUser = input.Adapt<ProfileDto, User>(user);
    //    var editIdentityResult = await _signInManager.UserManager.UpdateAsync(editUser);
    //    editIdentityResult.CheckErrors();

    //    return await OkAsync();
    //}



    //[HttpPatch]
    //public async Task<BaseResult> Signout()
    //{
    //    await _signInManager.SignOutAsync();
    //    return await OkAsync();
}


