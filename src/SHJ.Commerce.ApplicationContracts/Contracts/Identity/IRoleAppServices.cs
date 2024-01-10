using SHJ.BaseFramework.Shared;

namespace SHJ.Commerce.ApplicationContracts.Contracts.Identity;

public interface IRoleAppServices
{
    Task<BaseResult> Create(CreateRoleDto input);
    Task<BaseResult<RoleDto>> Edit(Guid id, EditRoleDto input);
    Task<BaseResult<RoleDto>> Get(Guid id);
    Task<BaseResult<RolesDto>> Get(BaseFilterDto input);
    Task<BaseResult> Delete(Guid id);
}
