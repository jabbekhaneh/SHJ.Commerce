using SHJ.BaseFramework.DependencyInjection.Contracts;
using SHJ.BaseFramework.Shared;

namespace SHJ.Commerce.ApplicationContracts.Contracts.Identity;

public interface IUserAppServices : ITransientDependency
{
    Task<BaseResult> Create(CreateUserDto input);
    Task<BaseResult> Edit(Guid id, EditUserDto input);
    Task<BaseResult<UsersDto>> Get(BaseFilterDto? filter);
    Task<BaseResult<UserDto>> Get(Guid id);
    Task<BaseResult> Delete(Guid id);
    
}
