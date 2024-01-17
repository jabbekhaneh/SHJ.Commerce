using SHJ.BaseFramework.DependencyInjection.Contracts;

namespace SHJ.Commerce.Shared.Shared.Identity;

public interface IPermissionRepository : ITransientDependency
{
    ICommandPermissionRepository Command { get; }
    IQueryPermissionRepository Query { get; }
    IPermissionDapperRepository Dapper { get; }
}
