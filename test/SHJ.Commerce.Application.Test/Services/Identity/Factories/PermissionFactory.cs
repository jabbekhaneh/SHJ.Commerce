using SHJ.Commerce.Application.Test.Configurations;
using SHJ.Commerce.ApplicationContracts.Contracts.Identity;
using System.Net;

namespace SHJ.Commerce.Application.Test.Services.Identity.Factories;

public class PermissionFactory
{
    public static async Task<List<PermissionDto>> GetPermissionsAsync(HttpClient client)
    {
        var permissions = await client.GetAsync(ApiConstUrls.PermissionAppServices);

        if (permissions.StatusCode != HttpStatusCode.OK)
        { throw new Exception("PermissionFactory.GeneratePermissions"); }

        var response = await permissions
            .DeserializeResponseAsync<BaseHttpResponseTestViewModel<List<PermissionDto>>>();
        
        return response.Result;
    }
}
