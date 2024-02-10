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
        string email = "dummy_email@mail.com".ToLower();
        var input = Builder<CreateUserDto>.CreateNew()
                                          .With(_ => _.UserName, email)
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
                                          .With(_ => _.UserName, email)
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

    [Fact]
    public async Task OnGetUserById_WhenExecuteController_ShouldReturnOK()
    {
        //arrange
        string email = "dummy-GetUserById@mail.com".ToLower();
        var createUserDto = Builder<CreateUserDto>.CreateNew()
                                                  .With(_ => _.UserName, email)
                                                  .With(_ => _.Password, "Aa@123456")
                                                  .Build();
        var res = await RequestClient.PostAsync(ApiConstUrls.UserAppServices, HttpHelper.GetJsonHttpContent(createUserDto));
        var response = await res.DeserializeResponseAsync<BaseHttpResponseTestViewModel<Guid>>();

        //act
        var actual = await RequestClient.GetAsync(_Sut + "/" + response.Result);

        //assert
        actual.StatusCode.Should().Be(HttpStatusCode.OK);
        var expected = await actual.DeserializeResponseAsync<BaseHttpResponseTestViewModel<UserDto>>();
        expected.Result.Email.Should().Be(email);
    }

    [Fact]
    public async Task OnGeAlltUsers_WhenExecuteController_ShouldReturnOK()
    {
        //arrange
        string email = "dummy-GetAllUser@mail.com".ToLower();
        var createUserDto = Builder<CreateUserDto>.CreateNew()
                                                  .With(_ => _.UserName, email)
                                                  .With(_ => _.Password, "Aa@123456")
                                                  .Build();
        await RequestClient.PostAsync(ApiConstUrls.UserAppServices, HttpHelper.GetJsonHttpContent(createUserDto));


        //act
        var actual = await RequestClient.GetAsync(_Sut);

        //assert
        actual.StatusCode.Should().Be(HttpStatusCode.OK);
        var response = await actual.DeserializeResponseAsync<BaseHttpResponseTestViewModel<UsersDto>>();
        response.Result.Users.First(_=>_.Email==email).Should().NotBeNull();
    }

    [Fact]
    public async Task OnGetUserById_WhenExecuteController_ShouldExceptionUserNotFound()
    {
        //arrange
        var userId = Guid.NewGuid();

        //act
        var actual = await RequestClient.GetAsync(_Sut + "/" + userId);

        //assert
        actual.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
    }



    [Fact]
    public async Task OnEditUserById_WhenExecutedController_ShouldReturnOK()
    {
        //arrange
        var createUserDto = Builder<CreateUserDto>.CreateNew()
                                                  .With(_ => _.UserName, "dummy-editUser@mail.com")
                                                  .With(_ => _.Password, "Aa@123456")
                                                  .Build();

        var createUserResponse = await RequestClient.PostAsync(_Sut, HttpHelper.GetJsonHttpContent(createUserDto));
        createUserResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await createUserResponse.DeserializeResponseAsync<BaseHttpResponseTestViewModel<Guid>>();
        Guid userId = result.Result;
        var editUserDto = Builder<EditUserDto>.CreateNew().With(_ => _.Job, "Developer").Build();

        //act
        var actual = await RequestClient.PutAsync(_Sut + "/" + userId, HttpHelper.GetJsonHttpContent(editUserDto));

        //assert
        actual.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}
