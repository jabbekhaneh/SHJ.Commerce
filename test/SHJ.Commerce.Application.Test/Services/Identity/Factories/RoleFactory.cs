using SHJ.Commerce.Application.Test.Configurations;
using SHJ.Commerce.ApplicationContracts.Contracts.Identity;

namespace SHJ.Commerce.Application.Test.Services.Identity.Factories;

internal class RoleFactory
{
    public static async Task<Guid> GenerateRoleAsync(HttpClient client, CreateRoleDto input)
    {
        var response = await client
            .PostAsync(ApiConstUrls.RoleAppServices, HttpHelper.GetJsonHttpContent(input));

        var roleId = await response.DeserializeResponseAsync<BaseHttpResponseTestViewModel<Guid>>();

        return roleId.Result;
    }
}
