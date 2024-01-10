using Testcontainers.MsSql;

namespace SHJ.Commerce.Application.Test.Configurations.Fixtures;

public class MssqlContainerFixture
{
    public MsSqlContainer Container { get; }
    public string DataSource { get; } = "localhost\\MsSqLServer2019,8002";

    public string DatabaseName { get; } = "dbTestApplication";
    public string UserID { get; } = "sa";

    public string Password { get; } = "Aa@123456";

    public MssqlContainerFixture()
    {
        Container = new MsSqlBuilder().Build();
    }

    public string GetConnectionString() => Container.GetConnectionString();



}