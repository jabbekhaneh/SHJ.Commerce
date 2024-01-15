using SHJ.BaseFramework.Shared;

namespace SHJ.Commerce.ApplicationContracts.Contracts.Identity;

public interface IRoleAppServices
{
    Task<BaseResult> Create(CreateRoleDto input);
    Task<BaseResult> Edit(Guid id, EditRoleDto input);
    Task<BaseResult> Get(Guid id);
    Task<BaseResult> Get(BaseFilterDto input);
    Task<BaseResult> Delete(Guid id);
}
