using FluentAssertions;
using SHJ.Commerce.Application.Test.Configurations.Fixtures;
using SHJ.Commerce.ApplicationContracts.Contracts.Identity;
using SHJ.Commerce.Shared.Common;
using System.Net;

namespace SHJ.Commerce.Application.Test.Services.Identity.v1;

public class AccountAppServices_Test : BaseControllerTests
{
    private readonly static string _Sut = ApiConstUrls.AccountAppServices;
    public AccountAppServices_Test(IntegrationContainersAppFactory factory) : base(factory)
    {

    }

    [Fact]
    public async Task OnSignIn_WhenExecuteController_ShouldReturnOK()
    {
        //arrange
        var signInDto = new SignIn()
        {
            IsPersistent = true,
            Password = UserAdminInfo.AdminPasswordDefaultValue,
            UserName = UserAdminInfo.AdminPasswordDefaultValue,
        };

        //act
        var actual = await RequestClient.PostAsync(_Sut, HttpHelper.GetJsonHttpContent(signInDto));

        //assert
        actual.StatusCode.Should().Be(HttpStatusCode.OK);
    }



}
