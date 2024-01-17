using SHJ.BaseFramework.DependencyInjection.Contracts;
using SHJ.BaseFramework.Repository;
using SHJ.Commerce.Domain.Aggregates.Identity;

namespace SHJ.Commerce.Shared.Shared.Identity;

public interface ICommandPermissionRepository : IBaseCommandRepository<Permission>, ITransientDependency
{

}
