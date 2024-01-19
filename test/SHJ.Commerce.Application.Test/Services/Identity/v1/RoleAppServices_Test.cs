using FizzWare.NBuilder;
using FluentAssertions;
using SHJ.Commerce.Application.Test.Configurations;
using SHJ.Commerce.Application.Test.Configurations.Fixtures;
using SHJ.Commerce.Application.Test.Services.Identity.Factories;
using SHJ.Commerce.ApplicationContracts.Contracts.Identity;
using System.Net;

namespace SHJ.Commerce.Application.Test.Services.Identity.v1;

public class RoleAppServices_Test : BaseControllerTests
{

    private readonly static string _Sut = ApiConstUrls.RoleAppServices;
    public RoleAppServices_Test(IntegrationContainersAppFactory factory) : base(factory)
    {

    }

    [Fact]
    public async Task OnCreateRole_WhenExecuteController_ShouldReturnOk()
    {
        //arrange

        var permissions = await PermissionFactory.GetPermissionsAsync(RequestHttp);
        var permissionIds = permissions.Select(_ => _.Id).ToList();

        var input = Builder<CreateRoleDto>.CreateNew()
                                          .With(_ => _.Name, "Dummy-RoleName")
                                          .With(_ => _.Permissions, permissionIds)
                                          .Build();

        //act
        var actual = await RequestHttp.PostAsync(_Sut, HttpHelper.GetJsonHttpContent(input));

        //assert
        actual.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task OnCreateRole_WhenExecuteController_ShouldExceptionDublicateTitle()
    {
        //arrange
        var permissions = await PermissionFactory.GetPermissionsAsync(RequestHttp);
        var permissionIds = permissions.Select(_ => _.Id).ToList();
        var input = Builder<CreateRoleDto>.CreateNew()
                                          .With(_ => _.Name, "Dummy-DublicateTitle")
                                          .With(_ => _.Permissions, permissionIds)
                                          .Build();
        await RoleFactory.GenerateRoleAsync(RequestHttp, input);


        //act
        var actual = await RequestHttp.PostAsync(_Sut, HttpHelper.GetJsonHttpContent(input));

        //assert
        actual.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
    }

    [Fact]
    public async Task OnDeleteRole_WhenExecuteController_ShouldReturnOK()
    {

        //arrange
        var input = new CreateRoleDto
        {
            Name = "Admin-Delete",
        };
        var response = await RequestHttp.PostAsync(_Sut, HttpHelper.GetJsonHttpContent(input));
        var roleId = await response.DeserializeResponseAsync<BaseHttpResponseTestViewModel<Guid>>();

        //act
        var actual = await RequestHttp.DeleteAsync(_Sut + "/" + roleId.Result);

        //assert
        actual.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task OnDeleteRole_WhenExecuteController_ShouldIdentityError_NotFoundRole()
    {

        //arrange
        Guid fakeId = Guid.NewGuid();

        //act
        var actual = await RequestHttp.DeleteAsync(_Sut + "/" + fakeId);

        //assert
        actual.StatusCode.Should().Be(HttpStatusCode.OK);
        var response = actual.DeserializeResponseAsync<BaseHttpResponseTestViewModel>();
        var executed = (int)HttpStatusCode.NotFound;
        response.Result.Status.Should().Be(executed);

    }

    [Fact]
    public async Task OnEditRole_WhenExecutedController_ShouldReturnRoleOK()
    {
        var input = new CreateRoleDto
        {
            Name = "Admin-Delete",
        };
        var response = await RequestHttp.PostAsync(_Sut, HttpHelper.GetJsonHttpContent(input));
        var roleId = await response.DeserializeResponseAsync<BaseHttpResponseTestViewModel<Guid>>();

        var editInput = new EditRoleDto
        {
            Name = "Admin-edit",
        };

        //act
        var actual = await RequestHttp.PutAsync(_Sut + "/" + roleId.Result, HttpHelper.GetJsonHttpContent(editInput));

        //assert
        actual.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task OnGetRoles_WhenExecutedController_ShouldReturnRolesOK()
    {
        //arrage
        var input = new CreateRoleDto
        {
            Name = "Admin-GetRoles",
        };
        var response = await RequestHttp.PostAsync(_Sut, HttpHelper.GetJsonHttpContent(input));
        var roleId = await response.DeserializeResponseAsync<BaseHttpResponseTestViewModel<Guid>>();
        //act
        var actual = await RequestHttp.GetAsync(_Sut);

        //assert
        var executed = await actual.DeserializeResponseAsync<BaseHttpResponseTestViewModel<RolesDto>>();
        executed.Result.Roles.First(_ => _.Name == input.Name).Should().NotBeNull();
    }

    [Fact]
    public async Task OnGetRoleById_WhenExecutedController_ShouldReturnRoleOK()
    {
        //arrage
        var input = new CreateRoleDto
        {
            Name = "Admin-GetRoleById",
        };
        var response = await RequestHttp.PostAsync(_Sut, HttpHelper.GetJsonHttpContent(input));
        var roleId = await response.DeserializeResponseAsync<BaseHttpResponseTestViewModel<Guid>>();

        //act
        var actual = await RequestHttp.GetAsync(_Sut + "/" + roleId.Result);

        //assert
        var executed = await actual.DeserializeResponseAsync<BaseHttpResponseTestViewModel<RoleDto>>();
        executed.Result.Name.Should().Be(input.Name);
    }
}
