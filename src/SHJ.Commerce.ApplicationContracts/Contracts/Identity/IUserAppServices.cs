using SHJ.BaseFramework.Shared;

namespace SHJ.Commerce.ApplicationContracts.Contracts.Identity;

public interface IUserAppServices
{
    Task<BaseResult> Create(CreateUserDto input);
    Task<BaseResult<UsersDto>> Get(BaseFilterDto input);
    Task<BaseResult<UserDto>> Get(Guid id);
    Task<BaseResult<UserDto>> Edit(Guid id, EditUserDto input);
    Task<BaseResult> Delete(Guid id);
    Task<BaseResult> DeleteRoles(DeleteRoleToUserDto input);
    Task<BaseResult> AddRoles(AddRoleToUserDto input);
}
