using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SHJ.BaseFramework.AspNet.Services;
using SHJ.BaseFramework.Repository;
using SHJ.BaseFramework.Shared;
using SHJ.Commerce.ApplicationContracts.Contracts.Identity;
using SHJ.Commerce.Domain.Aggregates.Identity;
using System.Security.Claims;

namespace SHJ.Commerce.Application.Services.Identity.v1;

[BaseControllerName("Role"),Authorize]
public class RoleAppServices : BaseAppService, IRoleAppServices
{
    private readonly RoleManager<Role> _Manager;

    public RoleAppServices(RoleManager<Role> roleManager)
    {
        _Manager = roleManager;
    }

    [HttpPost]
    public async Task<BaseResult> Create([FromBody]CreateRoleDto input)
    {
        if (!ModelState.IsValid)
            return await FailRequestAsync(ModelState);

        var newRole = new Role
        {
            Name = input.Name,
        };

        var addRoleResult = await _Manager.CreateAsync(newRole);
        
        addRoleResult.CheckErrors();
        

        await AddClaims(input, newRole);

        return await ReturnResultAsync(newRole.Id);
    }

    [HttpDelete("{id}")]
    public async Task<BaseResult> Delete([FromRoute]Guid id)
    {
        var role = await _Manager.FindByIdAsync(id.ToString());

        if (role == null) return await FailRequestAsync(BaseStatusCodes.NotFound);

        var result = await _Manager.DeleteAsync(role);

        result.CheckErrors();

        return await OkAsync();
    }

    [HttpPut("{id}")]
    public async Task<BaseResult> Edit([FromRoute]Guid id,[FromBody] EditRoleDto input)
    {
        if (!ModelState.IsValid)
            return await FailRequestAsync(ModelState);

        var role = await _Manager.FindByIdAsync(id.ToString());

        if (role == null) return await FailRequestAsync(BaseStatusCodes.NotFound);

        role.Name = input.Name;

        

        var getClaimes = await _Manager.GetClaimsAsync(role);

        var removePermissions = getClaimes.Where(_ => input.Permissions.Any(p => p.ToString() != _.Value));

        foreach (var permission in removePermissions)
        {
            await _Manager.RemoveClaimAsync(role, new Claim("Permission", permission.ToString()));
        }  
        
        foreach (var permission in input.Permissions)
        {
            await _Manager.AddClaimAsync(role, new Claim("Permission", permission.ToString()));
        }
        var result = await _Manager.UpdateAsync(role);

        result.CheckErrors();

        return await ReturnResultAsync(result);
    }

    [HttpGet("{id}")]
    public async Task<BaseResult> Get([FromRoute] Guid id)
    {
        var role = await _Manager.FindByIdAsync(id.ToString());

        if (role == null) return await FailRequestAsync(BaseStatusCodes.NotFound);

        return await ReturnResultAsync(role);
    }


    [HttpGet]
    public async Task<BaseResult> Get([FromRoute]BaseFilterDto input)
    {
        var result = new RolesDto();
        var query = _Manager.Roles;
        if (!string.IsNullOrEmpty(input.Search))
            query = query.Where(_ => _.Name.Contains(input.Search) ||
                                     _.NormalizedName.Contains(input.Search));

        var paging = query.Pagination<Role>(input.Take ?? 40, input.PageId ?? 1);
        result.PageSize = paging.PageSize;
        result.Roles = await paging.Query.Select(_ => new RoleDto
        {
            Name = _.Name,
            Id = _.Id,
        }).ToListAsync();

        return await ReturnResultAsync(result);
    }


    #region Private Methods
    private async Task AddClaims(CreateRoleDto input, Role newRole)
    {
        foreach (var permission in input.Permissions)
        {
            await _Manager
                .AddClaimAsync(newRole, new Claim("Permission", permission.ToString()));
        }
    }
    #endregion
}
