using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SHJ.BaseFramework.Shared;
using SHJ.Commerce.ApplicationContracts.Contracts.Identity;
using SHJ.Commerce.Domain;
using SHJ.Commerce.Domain.Aggregates.Identity;
using SHJ.Commerce.Domain.Shared;
using SHJ.Commerce.Infrastructure;
using SHJ.ExceptionHandler;

namespace SHJ.Commerce.Application.Services.Identity.v1;

[BaseControllerName("Account")]
public class AccountAppServices : IAccountAppServices
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public AccountAppServices(SignInManager<User> signInManager, UserManager<User> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    public async Task SingUp(SignUpDto input)
    {
        if (_userManager.FindByEmailAsync(input.Email.ToLower()) != null)
            throw new BaseBusinessException(GlobalIdentityErrors.DublicationEmail);

        await _userManager.CreateAsync(new User
        {
            Email = input.Email,
            FirstName = input.FirstName,
            LastName = input.LastName,

        }, input.Password);

        
    }




    public async Task<BaseResult> SignIn([FromBody] SignInDto input)
    {


        return null;
    }

    [HttpGet, Authorize]
    public async Task<BaseResult<ProfileDto>> Profile()
    {
        //var userId = ClaimService.GetUserId().ToString();

        //var user = _signInManager.UserManager.FindByIdAsync(userId);
        //var profile = user.Adapt<ProfileDto>();

        //return await OkAsync<ProfileDto>(profile);
        return null;
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


