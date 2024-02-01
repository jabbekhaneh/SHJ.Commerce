using SHJ.Commerce.Application.Test.Configurations;
using SHJ.Commerce.ApplicationContracts.Contracts.Identity;
using System.Net;

namespace SHJ.Commerce.Application.Test.Services.Identity;

public static class PermissionExtentions
{
    public static async Task<List<Guid>> GetPermissionsAsync(this HttpClient client)
    {
        var permissions = await client.GetAsync(ApiConstUrls.PermissionAppServices);

        if (permissions.StatusCode != HttpStatusCode.OK)
            throw new Exception("get permission error");

        var response = await permissions
            .DeserializeResponseAsync<BaseHttpResponseTestViewModel<List<PermissionDto>>>();

        return response.Result.Select(_ => _.Id).ToList();
    }
}
