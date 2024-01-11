using Microsoft.AspNetCore.Identity;
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


    public static string CheckSignInResult(this SignInResult signInResult)
    {
        if (signInResult.Succeeded)
        {
            return "Succeeded";
        }

        if (signInResult.IsLockedOut)
        {

            return "IsLockedOut";
        }

        if (signInResult.IsNotAllowed)
        {
            return "IsNotAllowed";
        }

        if (signInResult.RequiresTwoFactor)
        {
            return "RequiresTwoFactor";
        }
        return "Unknown";
    }
}
