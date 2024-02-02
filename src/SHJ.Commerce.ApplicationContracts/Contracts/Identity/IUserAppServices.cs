using SHJ.BaseFramework.DependencyInjection.Contracts;
using SHJ.BaseFramework.Shared;

namespace SHJ.Commerce.ApplicationContracts.Contracts.Identity;

public interface IUserAppServices : ITransientDependency
{
    Task<BaseResult> Create(CreateUserDto input);
    Task<BaseResult<UserDto>> GetById(Guid id);
    Task<BaseResult> Delete(Guid id);
    
}
