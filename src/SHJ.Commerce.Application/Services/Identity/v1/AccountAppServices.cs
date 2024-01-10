using Microsoft.AspNetCore.Mvc;
using SHJ.Commerce.ApplicationContracts.Contracts.Identity;

namespace SHJ.Commerce.Application.Services.Identity.v1;

[ControllerName("Account")]
public class AccountAppServices : IAccountAppServices
{
    public Task SignIn(SignIn input)
    {
        throw new NotImplementedException();
    }

    public Task SignOut()
    {
        throw new NotImplementedException();
    }

    public Task SignUp(SignUp input)
    {
        throw new NotImplementedException();
    }
}
