using SHJ.Commerce.ApplicationContracts.Contracts.Identity;
using System.Data;
using System.Data.SqlClient;
using System.Net.Http.Headers;


namespace SHJ.Commerce.Application.Test;

public class BaseControllerTests : IClassFixture<IntegrationContainersAppFactory>
{
    private readonly IntegrationContainersAppFactory _factory;
    public readonly HttpClient RequestClient;
    protected IDbConnection Connection { get; set; }
    public BaseControllerTests(IntegrationContainersAppFactory factory)
    {
        _factory = factory;
        RequestClient = _factory.CreateClient();
        Connection = new SqlConnection(_factory.SqlContainerFixture.GetConnectionString);
        
    }

    //private async Task GetToken()
    //{
    //    string email = "username@mail.com".ToLower();
    //    string password = "Aa@123456";
    //    var input = Builder<CreateUserDto>.CreateNew()
    //                                      .With(_ => _.Email, email)
    //                                      .With(_ => _.Password, password)
    //                                      .Build();

    //    var response = await RequestClient.PostAsync(ApiConstUrls.UserAppServices, HttpHelper.GetJsonHttpContent(input));
    //    response.StatusCode.Should().Be(HttpStatusCode.OK);
    //    var signInDto = new SignInDto()
    //    {
    //        IsPersistent = true,
    //        UserName = email,
    //        Password = password,
    //    };

    //    var actual = await RequestClient.PostAsync(ApiConstUrls.AccountAppServices, HttpHelper.GetJsonHttpContent(signInDto));
    //    var token = await actual.DeserializeResponseAsync<BaseHttpResponseTestViewModel<string>>();

    //    RequestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Result);
    //}


}

