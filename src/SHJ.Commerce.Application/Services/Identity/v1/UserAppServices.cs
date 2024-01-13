using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SHJ.BaseFramework.AspNet.Services;
using SHJ.BaseFramework.Shared;
using SHJ.Commerce.ApplicationContracts.Contracts.Identity;
using SHJ.Commerce.Domain.Aggregates.Identity;

namespace SHJ.Commerce.Application.Services.Identity.v1;

[BaseControllerName("User")]
public class UserAppServices : BaseAppService, IUserAppServices
{
    private readonly UserManager<User> _userManager;
    public UserAppServices(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    [HttpPost("Roles")]
    public Task<BaseResult> AddRoles(AddRoleToUserDto input)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public Task<BaseResult> Create()
    {
        throw new NotImplementedException();
    }

    [HttpDelete("{id}")]
    public Task<BaseResult> Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    [HttpDelete("{id}/Roles")]
    public Task<BaseResult> DeleteRoles(DeleteRoleToUserDto input)
    {
        throw new NotImplementedException();
    }

    [HttpPut("{id}")]
    public Task<BaseResult<UserDto>> Edit(Guid id, EditUserDto input)
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    public Task<BaseResult<UsersDto>> Get(BaseFilterDto input)
    {
        throw new NotImplementedException();
    }

    [HttpGet("{id}")]
    public Task<BaseResult<UserDto>> Get(Guid id)
    {
        var query = _userManager.Users;
        throw new NotImplementedException();
    }
}
