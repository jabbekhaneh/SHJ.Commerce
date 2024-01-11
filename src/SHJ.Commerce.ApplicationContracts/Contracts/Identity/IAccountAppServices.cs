using SHJ.BaseFramework.Shared;

namespace SHJ.Commerce.ApplicationContracts.Contracts.Identity;

public interface IAccountAppServices
{    
    Task<BaseResult> SignUp(SignUp input);
    Task<BaseResult> SignIn(SignIn input);
    Task Signout();
}
