
using Testcontainers.MsSql;

namespace SHJ.Commerce.Application.Test.Configurations.Fixtures;

public class MssqlContainerFixture
{
    public MsSqlContainer Container { get; }
    public const ushort MsSqlPort = 51433;
    public string DataSource { get; } = $"MsSqLServer2019,{MsSqlPort}";

    public string DatabaseName { get; } = "dbMaster";
    public string UserID { get; } = "sa";

    public string Password { get; } = "Aa@123456";

    //mcr.microsoft.com/mssql/server:2022-latest
    public string Image_Name { get; } = "mcr.microsoft.com/mssql/server:2019-latest";


    public MssqlContainerFixture()
    {
        Container = new MsSqlBuilder()
            //.WithImage(Image_Name)
            //.WithPortBinding(MsSqlPort)
            //.WithEnvironment("ACCEPT_EULA", "Y")
            //.WithEnvironment("SQLCMDUSER", UserID)
            //.WithEnvironment("SQLCMDPASSWORD", Password)
            //.WithEnvironment("MSSQL_SA_PASSWORD", Password)
            .Build();
    }

    public string GetConnectionString => Container.GetConnectionString();



}