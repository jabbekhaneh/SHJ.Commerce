using Microsoft.AspNetCore.Mvc;
using SHJ.BaseFramework.AspNet.Services;
using SHJ.BaseFramework.Shared;
using SHJ.Commerce.ApplicationContracts.Contracts.Identity;

namespace SHJ.Commerce.Application.Services.Identity.v1;

[ControllerName("User")]
public class UserAppServices : BaseAppService, IUserAppServices
{
    public UserAppServices()
    {

    }

    [HttpPost,ActionName("Roles")]
    public Task<BaseResult> AddRoles(AddRoleToUserDto input)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public Task<BaseResult> Create()
    {
        throw new NotImplementedException();
    }

    [HttpDelete]
    public Task<BaseResult> Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    [HttpDelete,ActionName("Roles")]
    public Task<BaseResult> DeleteRoles(DeleteRoleToUserDto input)
    {
        throw new NotImplementedException();
    }

    [HttpDelete("{id}")]
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
        throw new NotImplementedException();
    }
}
