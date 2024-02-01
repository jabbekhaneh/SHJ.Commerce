using SHJ.Commerce.ApplicationContracts.Contracts.Identity;


namespace SHJ.Commerce.Application.Test.Services.Identity.v1;

public class RoleAppServices_Test : BaseControllerTests
{

    private static string _Sut = ApiConstUrls.RoleAppServices;
    public RoleAppServices_Test(IntegrationContainersAppFactory factory) : base(factory)
    {

    }

    [Fact]
    public async Task OnCreateRole_WhenExecuteController_ShouldReturnOk()
    {
        //arrange

        var permissionIds = await RequestClient.GetPermissionsAsync();
        string roleName = "Dummy-Create-Role-Name";
        var input = Builder<CreateRoleDto>.CreateNew()
                                          .With(_ => _.Name, roleName)
                                          .With(_ => _.Permissions, permissionIds)
                                          .Build();
        //act
        var actual = await RequestClient.PostAsync(_Sut, HttpHelper.GetJsonHttpContent(input));

        //assert
        actual.StatusCode.Should().Be(HttpStatusCode.OK);

    }

    [Fact]
    public async Task OnCreateRole_WhenExecuteController_ShouldExceptionDublicateTitle()
    {
        //arrange
        var permissionIds = await RequestClient.GetPermissionsAsync();
        string roleName = "Dummy-DublicateTitle";
        await RequestClient.CreateRoleAsync(roleName, permissionIds);
        var input = Builder<CreateRoleDto>.CreateNew()
                                         .With(_ => _.Name, roleName)
                                         .With(_ => _.Permissions, permissionIds)
                                         .Build();

        //act
        var actual = await RequestClient.PostAsync(_Sut, HttpHelper.GetJsonHttpContent(input));

        //assert
        actual.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
    }

    [Fact]
    public async Task OnDeleteRole_WhenExecuteController_ShouldReturnOK()
    {

        //arrange
        var permissionIds = await RequestClient.GetPermissionsAsync();
        string roleName = "Dummy-Delete-Role-Name".ToLower();

        var roleId = await RequestClient.CreateRoleAsync(roleName, permissionIds);

        //act
        var actual = await RequestClient.DeleteAsync(_Sut + "/" + roleId);

        //assert
        actual.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task OnDeleteRole_WhenExecuteController_ShouldIdentityError_NotFoundRole()
    {
        //arrange
        Guid fakeId = Guid.NewGuid();

        //act
        var actual = await RequestClient.DeleteAsync(_Sut + "/" + fakeId);

        //assert
        actual.StatusCode.Should().Be(HttpStatusCode.OK);
        var response = actual.DeserializeResponseAsync<BaseHttpResponseTestViewModel>();
        var executed = (int)HttpStatusCode.NotFound;
        response.Result.Status.Should().Be(executed);
    }

    [Fact]
    public async Task OnEditRole_WhenExecutedController_ShouldReturnRoleOK()
    {
        //arrange
        var permissionIds = await RequestClient.GetPermissionsAsync();
        string roleName = "Dummy-Admin";
        var roleId = await RequestClient.CreateRoleAsync(roleName, permissionIds);
        var editInput = Builder<EditRoleDto>.CreateNew()
                                            .With(_ => _.Name, "Admin-edit")
                                            .With(_ => _.Permissions, permissionIds)
                                            .Build();

        //act
        var actual = await RequestClient.PutAsync(_Sut + "/" + roleId, HttpHelper.GetJsonHttpContent(editInput));

        //assert
        actual.StatusCode.Should().Be(HttpStatusCode.OK);
        var roles = await RequestClient.GetRoles();
        roles.First(_ => _.Id == roleId).Name.Should().Be(editInput.Name);
    }

    [Fact]
    public async Task OnGetRoles_WhenExecutedController_ShouldReturnRolesOK()
    {
        //arrage
        var permissionIds = await PermissionExtentions.GetPermissionsAsync(RequestClient);
        string roleName = "Dummy-GetRole";
        await RequestClient.CreateRoleAsync(roleName, permissionIds);

        //act
        var actual = await RequestClient.GetAsync(_Sut);

        //assert
        actual.StatusCode.Should().Be(HttpStatusCode.OK);
        var executed = await actual.DeserializeResponseAsync<BaseHttpResponseTestViewModel<RolesDto>>();
        executed.Result.Roles.Where(_ => _.Name == roleName).First().Name.Should().Be(roleName);
    }

    [Fact]
    public async Task OnGetRoleById_WhenExecutedController_ShouldReturnRoleOK()
    {
        //arrage
        var permissionIds = await PermissionExtentions.GetPermissionsAsync(RequestClient);
        string roleName = "Dummy-DeleteRole".ToLower();
        var roleId = await RequestClient.CreateRoleAsync(roleName, permissionIds);

        //act
        var actual = await RequestClient.GetAsync(_Sut + "/" + roleId);

        //assert
        actual.StatusCode.Should().Be(HttpStatusCode.OK);
        var executed = await actual.DeserializeResponseAsync<BaseHttpResponseTestViewModel<RoleDto>>();
        executed.Result.Name.Should().Be(roleName);
    }


}
