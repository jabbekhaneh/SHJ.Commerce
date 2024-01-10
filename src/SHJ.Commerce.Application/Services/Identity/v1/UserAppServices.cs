using Microsoft.AspNetCore.Mvc;
using SHJ.BaseFramework.AspNet.Services;
using SHJ.Commerce.ApplicationContracts.Contracts.Identity;

namespace SHJ.Commerce.Application.Services.Identity.v1;

[ControllerName("User")]
public class UserAppServices : BaseAppService, IUserAppServices
{
    public UserAppServices()
    {

    }
}
