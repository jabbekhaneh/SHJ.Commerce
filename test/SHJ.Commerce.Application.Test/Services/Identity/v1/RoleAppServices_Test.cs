using FluentAssertions;
using SHJ.Commerce.Application.Test.Configurations;
using SHJ.Commerce.Application.Test.Configurations.Fixtures;
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
        var permissions = await RequestHttp.GetAsync(ApiConstUrls.PermissionAppServices);
        var response = await permissions.DeserializeResponseAsync<HttpResponseViewModel<List<PermissionDto>>>();
        permissions.StatusCode.Should().Be(HttpStatusCode.OK);

        var input = new CreateRoleDto
        {
            Name = "Admin",
            Permissions = response.Result.Select(_ => _.Id).ToList(),
        };

        //act
        var actual = await RequestHttp.PostAsync(_Sut, HttpHelper.GetJsonHttpContent(input));

        //assert
        actual.StatusCode.Should().Be(HttpStatusCode.OK);

    }

    [Fact]
    public async Task OnCreateRole_WhenExecuteController_ShouldExceptionDublicateTitle()
    {
        //arrange
        var input = new CreateRoleDto
        {
            Name = "Admin-Dummy",
        };
        await RequestHttp.PostAsync(_Sut, HttpHelper.GetJsonHttpContent(input));


        //act
        var actual = await RequestHttp.PostAsync(_Sut, HttpHelper.GetJsonHttpContent(input));

        //assert
        actual.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
    }

}
