﻿using SHJ.BaseFramework.DependencyInjection.Contracts;
using SHJ.BaseFramework.Shared;

namespace SHJ.Commerce.ApplicationContracts.Contracts.Identity;

public interface IAccountAppServices : IScopedDependency
{

    Task SingUp(SignUpDto input);
    Task<BaseResult<ProfileDto>> Profile();
    //Task<BaseResult> Profile(ProfileDto input);
    //Task<BaseResult> Signout();

}
