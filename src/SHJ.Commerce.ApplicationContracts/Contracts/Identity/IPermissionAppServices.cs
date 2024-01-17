using SHJ.BaseFramework.Shared;

namespace SHJ.Commerce.ApplicationContracts.Contracts.Identity;

public interface IPermissionAppServices 
{
    Task<BaseResult> Get();
}
