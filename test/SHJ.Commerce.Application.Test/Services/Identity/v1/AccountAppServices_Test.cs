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
        //arrange
        string email = "SignIn@mail.com".ToLower();
        string password = "Aa@123456";
        var input = Builder<CreateUserDto>.CreateNew()
                                          .With(_ => _.Email, email)
                                          .With(_ => _.Password,password)
                                          .Build();

        var response = await RequestClient.PostAsync(ApiConstUrls.UserAppServices, HttpHelper.GetJsonHttpContent(input));
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var signInDto = new SignInDto()
        {
            IsPersistent = true,
            UserName = email,
            Password = password,
        };

        //act
        var actual = await RequestClient.PostAsync(_Sut, HttpHelper.GetJsonHttpContent(signInDto));

        //assert
        actual.StatusCode.Should().Be(HttpStatusCode.OK);
    }



}
