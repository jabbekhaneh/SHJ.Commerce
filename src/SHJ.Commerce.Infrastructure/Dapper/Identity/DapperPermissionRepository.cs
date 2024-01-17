using Microsoft.Extensions.Options;
using SHJ.BaseFramework.Dapper;
using SHJ.BaseFramework.Shared;
using SHJ.Commerce.Domain.Aggregates.Identity;
using SHJ.Commerce.Shared.Shared.Identity;

namespace SHJ.Commerce.Infrastructure.Dapper.Identity;

public class DapperPermissionRepository : BaseQueryDapperSqlServerData<Permission>, IPermissionDapperRepository
{
    public DapperPermissionRepository(IOptions<BaseOptions> options) : base(options)
    {
    }

}
