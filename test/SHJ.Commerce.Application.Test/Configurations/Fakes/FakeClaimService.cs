using SHJ.BaseFramework.Shared;

namespace SHJ.Commerce.Application.Test.Configurations.Fakes;

public class FakeClaimService : IBaseClaimService
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
