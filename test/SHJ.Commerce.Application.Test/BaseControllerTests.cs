using System.Data;
using System.Data.SqlClient;


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

}

