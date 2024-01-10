using Microsoft.AspNetCore.Mvc;
using SHJ.BaseFramework.AspNet.Services;
using SHJ.BaseFramework.Shared;
using SHJ.Commerce.ApplicationContracts.Contracts.Identity;

namespace SHJ.Commerce.Application.Services.Identity.v1;

[ControllerName("Role")]
public class RoleAppServices : BaseAppService, IRoleAppServices
{
    public Task<BaseResult> Create(CreateRoleDto input)
    {
        throw new NotImplementedException();
    }

    public Task<BaseResult> Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<BaseResult<RoleDto>> Edit(Guid id, EditRoleDto input)
    {
        throw new NotImplementedException();
    }

    public Task<BaseResult<RoleDto>> Get(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<BaseResult<RolesDto>> Get(BaseFilterDto input)
    {
        throw new NotImplementedException();
    }
}
