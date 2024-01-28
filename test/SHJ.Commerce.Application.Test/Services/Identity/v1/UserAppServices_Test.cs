
using SHJ.Commerce.ApplicationContracts.Contracts.Identity;

namespace SHJ.Commerce.Application.Test.Services.Identity.v1;

public class UserAppServices_Test : BaseControllerTests
{

    private readonly static string _Sut = ApiConstUrls.UserAppServices;
    public UserAppServices_Test(IntegrationContainersAppFactory factory) : base(factory)
    {

    }

    [Fact]
    public async Task OnCreateUser_WhenExecuteController_ShouldReturnOK()
    {
        //arrange
        string email = "dummy-email@mail.com".ToLower();
        string roleName = "RoleForCreateUser".ToLower();
        var permissionIds = await RequestHttp.GetPermissionsAsync();
        await RequestHttp.CreateRoleAsync(roleName, permissionIds);
        var roles = new List<string>();
        roles.Add(roleName);
        var createUserDto = Builder<CreateUserDto>.CreateNew()
                                                  .With(_ => _.Email, email)
                                                  .With(_ => _.Password, "Aa@123456")
                                                  .With(_ => _.RoleNames, roles)
                                                  .Build();


        //act 
        var actual = await RequestHttp.PostAsync(_Sut, HttpHelper.GetJsonHttpContent(createUserDto));

        //assert
        actual.StatusCode.Should().Be(HttpStatusCode.OK);
    }


    [Fact]
    public async Task OnCreateUser_WhenExecuteController_ShouldExceptionDublicateUserName()
    {
        //arrange
        string email = "dummy-DublicateEmail@mail.com".ToLower();
        await RequestHttp.CreateUserAsync(email);
        var createUserDto = Builder<CreateUserDto>.CreateNew()
                                                  .With(_ => _.Email, email)
                                                  .With(_ => _.Password, "Aa@123456")
                                                  .Build();

        //act
        var actual = await RequestHttp.PostAsync(_Sut, HttpHelper.GetJsonHttpContent(createUserDto));

        //assert
        actual.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
    }

    [Fact]
    public async Task OnDeleteUser_WhenExecuteController_ShouldReturnOK()
    {
        //arrange
        string email = "dummy-DeleteUser@mail.com".ToLower();
        Guid userId = await RequestHttp.CreateUserAsync(email);

        //act 
        var actual = await RequestHttp.DeleteAsync(_Sut + "/" + userId );

        //assert
        actual.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}
