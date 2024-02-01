using SHJ.BaseFramework.Shared;

namespace SHJ.Commerce.ApplicationContracts.Contracts.Identity;

public interface IAccountAppServices
{    
    Task<BaseResult> SignIn(SignIn input);
    
}
