using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SHJ.BaseFramework.AspNet.Services;
using SHJ.BaseFramework.Repository;
using SHJ.BaseFramework.Shared;
using SHJ.Commerce.ApplicationContracts.Contracts.Identity;
using SHJ.Commerce.Domain;
using SHJ.Commerce.Domain.Aggregates.Identity;
using SHJ.Commerce.Infrastructure;
using SHJ.ExceptionHandler;

namespace SHJ.Commerce.Application.Services.Identity.v1;

[BaseControllerName("User"),Authorize]
public class UserAppServices : BaseAppService, IUserAppServices
{
    private readonly UserManager<User> _Manager;
    public UserAppServices(UserManager<User> userManager)
    {
        _Manager = userManager;
    }

    [HttpPost]
    public async Task<BaseResult> Create([FromBody] CreateUserDto input)
    {
        if (!ModelState.IsValid)
            return await FailRequestAsync(ModelState);

        var newUser = input.Adapt<User>();
        newUser.UserName = input.UserName;
        newUser.Email = input.UserName.ToLower();
        newUser.MobileNumberConfirmed = false;
        var createUser = await _Manager.CreateAsync(newUser, input.Password);
        createUser.CheckErrors();

        if (input.RoleNames.Any())
        {
            var addRolesToUser = await _Manager.AddToRolesAsync(newUser, input.RoleNames);
            addRolesToUser.CheckErrors();
        }

        return await ReturnResultAsync(newUser.Id);
    }

    [HttpDelete("{id}")]
    public async Task<BaseResult> Delete([FromRoute] Guid id)
    {
        var user = await _Manager.FindByIdAsync(id.ToString());
        if (user is null) throw new BaseBusinessException(GlobalIdentityErrors.UserNotFound);
        var result = await _Manager.DeleteAsync(user);
        result.CheckErrors();
        return await OkAsync();
    }

    [HttpPut("{id}")]
    public async Task<BaseResult> Edit([FromRoute] Guid id, [FromBody] EditUserDto input)
    {
        var user = await _Manager.FindByIdAsync(id.ToString());
        if (user is null) throw new BaseBusinessException(GlobalIdentityErrors.UserNotFound);

        var editUser = input.Adapt<EditUserDto, User>(user);

        var result = await _Manager.UpdateAsync(editUser);
        result.CheckErrors();

        var addRolesResult = await _Manager.AddToRolesAsync(user, input.RoleNames);
        addRolesResult.CheckErrors();

        return await OkAsync();
    }

    [HttpGet]
    public async Task<BaseResult<UsersDto>> Get([FromRoute] BaseFilterDto? filter)
    {
        var result = new UsersDto();
        IQueryable<User> users = _Manager.Users;

        if (filter?.Search is not null)
        {
            users = users.Where(_ => _.Email.Contains(filter.Search));
        }
        var paging = users.Pagination<User>(filter.Take ?? 40, filter.PageId ?? 1);

        result.Users = await paging.Query.ProjectToType<UserDto>().ToListAsync();
        result.PageSize = paging.PageSize;
        return await OkAsync<UsersDto>(result);
    }

    [HttpGet("{id}")]
    public async Task<BaseResult<UserDto>> Get([FromRoute] Guid id)
    {
        var user = await _Manager.FindByIdAsync(id.ToString());
        if (user is null) throw new BaseBusinessException(GlobalIdentityErrors.UserNotFound);
        var userInfo = user.Adapt<UserDto>();
        return await OkAsync<UserDto>(userInfo);
    }


}
