using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SHJ.BaseFramework.AspNet.Services;
using SHJ.BaseFramework.Shared;
using SHJ.Commerce.ApplicationContracts.Contracts.Identity;
using SHJ.Commerce.Domain;
using SHJ.Commerce.Domain.Aggregates.Identity;
using SHJ.ExceptionHandler;

namespace SHJ.Commerce.Application.Services.Identity.v1;

[BaseControllerName("User")]
public class UserAppServices : BaseAppService, IUserAppServices
{
    private readonly UserManager<User> _Manager;
    
    public UserAppServices(UserManager<User> userManager)
    {
        _Manager = userManager;   
    }

    [HttpPost]
    public async Task<BaseResult> Create([FromBody]CreateUserDto input)
    {
        if (!ModelState.IsValid)
            return await FailRequestAsync(ModelState);

        var newUser = input.Adapt<User>();
        newUser.UserName = input.Email;
        newUser.MobileNumberConfirmed=false;

        var createUser = await _Manager.CreateAsync(newUser, input.Password);
        createUser.CheckErrors();

        var addRolesToUser = await _Manager.AddToRolesAsync(newUser, input.RoleNames);
        addRolesToUser.CheckErrors();

        return await ReturnResultAsync(newUser.Id);
    }

    [HttpDelete("{id}")]
    public async Task<BaseResult> Delete([FromRoute]Guid id)
    {
        var user = await _Manager.FindByIdAsync(id.ToString());
        if(user is null) throw new BaseBusinessException(GlobalIdentityErrors.UserNotFound);
        var result = await _Manager.DeleteAsync(user);
        result.CheckErrors();
        return await OkAsync();
    }

   
}
