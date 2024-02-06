using SHJ.BaseFramework.Shared;

namespace SHJ.Commerce.ApplicationContracts.Contracts.Identity;

public interface IAccountAppServices
{
    Task<BaseResult> SignIn(SignInDto input);   
    //Task<BaseResult<ProfileDto>> Profile();
    //Task<BaseResult> Profile(ProfileDto input);
    //Task<BaseResult> Signout();

}
