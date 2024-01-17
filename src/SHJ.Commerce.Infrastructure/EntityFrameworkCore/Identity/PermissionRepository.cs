using SHJ.Commerce.Shared.Shared.Identity;

namespace SHJ.Commerce.Infrastructure.EntityFrameworkCore.Identity;

public class PermissionRepository : IPermissionRepository
{
    public ICommandPermissionRepository Command { get; }
    public IQueryPermissionRepository Query { get; }

    public IPermissionDapperRepository Dapper { get; }

    public PermissionRepository(ICommandPermissionRepository command, IQueryPermissionRepository query, IPermissionDapperRepository dapper)
    {
        Command = command;
        Query = query;
        Dapper = dapper;
    }
}
