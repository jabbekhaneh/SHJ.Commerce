using Microsoft.AspNetCore.Identity;
using SHJ.Commerce.Domain;
using SHJ.ExceptionHandler;
using System.Collection;

namespace SHJ.Commerce.Application.Services.Identity;

public static class IdentityExtensions
{
    public static void CheckErrors(this IdentityResult identityResult)
    {
        if (identityResult.Succeeded)
        {
            return;
        }

        if (identityResult.Errors == null)
        {
            throw new ArgumentException("identityResult.Errors should not be null.");
        }

        throw new BaseBusinessException(
            code: "Identity Error Code : " + identityResult.Errors.First().Code,
            message: identityResult.Errors.Select(err => err.Description).JoinAsString(", "));
    }


    public static void CheckSignInResult(this SignInResult signInResult)
    {
        if (signInResult.Succeeded)
            return;

        if (signInResult.IsLockedOut)
            throw new BaseBusinessException(GlobalIdentityErrors.IsLockedOut, "IsLockedOut");

        if (signInResult.IsNotAllowed)
            throw new BaseBusinessException(GlobalIdentityErrors.IsNotAllowed, "IsNotAllowed");

        if (signInResult.RequiresTwoFactor)
            throw new BaseBusinessException(GlobalIdentityErrors.RequiresTwoFactor, "RequiresTwoFactor");

        throw new BaseBusinessException(GlobalIdentityErrors.Name);
    }
}
