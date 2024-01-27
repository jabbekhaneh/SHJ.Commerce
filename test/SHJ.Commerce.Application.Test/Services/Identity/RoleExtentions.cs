using SHJ.Commerce.ApplicationContracts.Contracts.Identity;

namespace SHJ.Commerce.Application.Test.Services.Identity;

internal static class RoleExtentions
{
    public static async Task<Guid> CreateRoleAsync(this HttpClient client, string roleName, List<Guid> permissionIds)
    {
        var input = Builder<CreateRoleDto>.CreateNew()
                                          .With(_ => _.Name, roleName)
                                          .With(_ => _.Permissions, permissionIds)
                                          .Build();

        var response = await client
            .PostAsync(ApiConstUrls.RoleAppServices, HttpHelper.GetJsonHttpContent(input));

        var roleId = await response.DeserializeResponseAsync<BaseHttpResponseTestViewModel<Guid>>();

        return roleId.Result;
    }

    public static async Task<List<RoleDto>> GetRoles(this HttpClient client)
    {
        var actual = await client.GetAsync(ApiConstUrls.RoleAppServices);
        actual.StatusCode.Should().Be(HttpStatusCode.OK);
        var executed = await actual.DeserializeResponseAsync<BaseHttpResponseTestViewModel<RolesDto>>();
        return executed.Result.Roles;
    }


}
