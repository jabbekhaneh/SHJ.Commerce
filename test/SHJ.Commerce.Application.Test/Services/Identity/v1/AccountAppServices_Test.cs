using SHJ.Commerce.ApplicationContracts.Contracts.Identity;
using SHJ.Commerce.Shared.Common;

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
        await RequestClient.CreateUserAsync("singIn@mail.com");
        //arrange
        var signInDto = new SignInDto()
        {
            IsPersistent = true,
            UserName = UserAdminInfo.AdminEmailDefaultValue,
            Password = UserAdminInfo.AdminPasswordDefaultValue,
        };

        //act
        var actual = await RequestClient.PostAsync(_Sut + "/SingIn", HttpHelper.GetJsonHttpContent(signInDto));

        //assert
        actual.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task OnGetProfile_WhenExecuteController_ShouldReturnOK()
    {

        //arrange


        //act
        var actual = await RequestClient.GetAsync(_Sut);

        //assert
        actual.StatusCode.Should().Be(HttpStatusCode.OK);
    }


}
