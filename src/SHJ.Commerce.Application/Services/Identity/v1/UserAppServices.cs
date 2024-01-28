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
    private readonly UserManager<User> _Manager;

    public UserAppServices(UserManager<User> userManager)
    {
        _Manager = userManager;
    }

    [HttpPost("Roles")]
    public Task<BaseResult> AddRoles(AddRoleToUserDto input)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public async Task<BaseResult> Create(CreateUserDto input)
    {
        if (!ModelState.IsValid)
            return await FailRequestAsync(ModelState);

        var newUser = new User
        {
            Email = input.Email,
            FirstName = input.FirstName,
            LastName = input.LastName,
            UserName = input.Email,
            MobileNumberConfirmed = false,
        };

        var createUser = await _Manager.CreateAsync(newUser, input.Password);
        createUser.CheckErrors();

        var addRolesToUser = await _Manager.AddToRolesAsync(newUser, input.RoleNames);
        addRolesToUser.CheckErrors();

        return await ResultAsync(newUser.Id);
    }

    [HttpDelete("{id}")]
    public async Task<BaseResult> Delete(Guid id)
    {
        var user = await _Manager.FindByIdAsync(id.ToString());
        var result = await _Manager.DeleteAsync(user);
        result.CheckErrors();
        return await OkAsync();
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
        var query = _Manager.Users;
        throw new NotImplementedException();
    }
}
