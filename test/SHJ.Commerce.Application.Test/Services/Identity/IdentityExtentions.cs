using SHJ.Commerce.ApplicationContracts.Contracts.Identity;

namespace SHJ.Commerce.Application.Test.Services.Identity;
internal static class IdentityExtentions
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

    public static async Task<Guid> CreateRoleAsync(this HttpClient client, string roleName, List<Guid> permissionIds)
    {
        var input = Builder<CreateRoleDto>.CreateNew()
                                          .With(_ => _.Name, roleName)
                                          .With(_ => _.Permissions, permissionIds)
                                          .Build();

        var response = await client.PostAsync(ApiConstUrls.RoleAppServices, HttpHelper.GetJsonHttpContent(input));

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

    public static async Task<Guid> CreateUserAsync(this HttpClient client, string email)
    {
        var createUserDto = Builder<CreateUserDto>.CreateNew()
                                                  .With(_ => _.Email, email)
                                                  .With(_ => _.Password, "Aa@123456")
                                                  .Build();

        var actual = await client.PostAsync(ApiConstUrls.UserAppServices, HttpHelper.GetJsonHttpContent(createUserDto));

        var response = await actual.DeserializeResponseAsync<BaseHttpResponseTestViewModel<Guid>>();
        return response.Result;
    }

}
