using SHJ.BaseFramework.Shared;

namespace SHJ.Commerce.Domain.Test.Configurations.Fakes;


public class ClaimServiceFake : IBaseClaimService
{
    public string GetClaim(string key)
    {
        return "Fake_Claim";
    }

    public string GetUserId()
    {
        return Guid.NewGuid().ToString();
    }

    public bool IsAuthenticated()
    {
        return false;
    }
}
