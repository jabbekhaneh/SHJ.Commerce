using SHJ.Commerce.ApplicationContracts.Contracts.Identity;

namespace SHJ.Commerce.Application.Test.Services.Identity;

public static class UserExtentions
{
    public static async Task<Guid> CreateUserAsync(this HttpClient client, string email)
    {
        var random=new Random();
        var permissionIds = await client.GetPermissionsAsync();
        string roleName = $"Admin_Admini{random.Next(0,1000)}".ToLower();
        await client.CreateRoleAsync(roleName, permissionIds);
        var roles = new List<string>();
        roles.Add(roleName);

        var createUserDto = Builder<CreateUserDto>.CreateNew()
                                                  .With(_ => _.Email, email)
                                                  .With(_ => _.RoleNames, roles)
                                                  .With(_ => _.Password, "Aa@123456")
                                                  .Build();

        var actual = await client.PostAsync(ApiConstUrls.UserAppServices, HttpHelper.GetJsonHttpContent(createUserDto));

        var response = await actual.DeserializeResponseAsync<BaseHttpResponseTestViewModel<Guid>>();
        return response.Result;
    }
}
