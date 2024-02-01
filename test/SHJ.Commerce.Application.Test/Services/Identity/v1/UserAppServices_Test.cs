using SHJ.Commerce.ApplicationContracts.Contracts.Identity;

namespace SHJ.Commerce.Application.Test.Services.Identity.v1;

public class UserAppServices_Test : BaseControllerTests
{

    private static string _Sut = ApiConstUrls.UserAppServices;
    public UserAppServices_Test(IntegrationContainersAppFactory factory) : base(factory)
    {

    }

    [Fact]
    public async Task OnCreateUser_WhenExecuteController_ShouldReturnOK()
    {
        //arrange
        string email = "dummy-email@mail.com".ToLower();
        var input = Builder<CreateUserDto>.CreateNew()
                                          .With(_ => _.Email, email)
                                          .With(_ => _.Password, "Aa@123456")
                                          .Build();
        //act 
        var actual = await RequestClient.PostAsync(_Sut, HttpHelper.GetJsonHttpContent(input));

        //assert
        actual.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task OnCreateUser_WhenExecuteController_ShouldExceptionDublicateUserName()
    {
        //arrange
        string email = "dummy-emaile-dublicate@mail.com".ToLower();
        await RequestClient.CreateUserAsync(email);
        var input = Builder<CreateUserDto>.CreateNew()
                                          .With(_ => _.Email, email)
                                          .With(_ => _.Password, "Aa@123456")
                                          .Build();
        //act 
        var actual = await RequestClient.PostAsync(_Sut, HttpHelper.GetJsonHttpContent(input));

        //assert
        actual.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
    }

    [Fact]
    public async Task OnCreateUser_WhenExecuteController_ShouldExceptionNotFoundUser()
    {
        //arrange
        var userIdFake = Guid.NewGuid();

        //act
        var actual = await RequestClient.DeleteAsync(_Sut + "/" + userIdFake);

        //assert
        actual.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
    }

    [Fact]
    public async Task OnDeleteUser_WhenExecuteController_ShouldReturnOK()
    {
        //arrange
        string email = "dummy-DeleteUser@mail.com".ToLower();
        Guid userId = await RequestClient.CreateUserAsync(email);

        //act 
        var actual = await RequestClient.DeleteAsync(_Sut + "/" + userId);

        //assert
        actual.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}
