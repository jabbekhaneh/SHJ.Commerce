using SHJ.ExceptionHandler;
using System.Linq;

namespace SHJ.Commerce.Domain.Aggregates.Identity;

public class PermissionManager : BaseDomainService<Permission>
{
    public PermissionManager(IBaseCommandRepository<Permission> commandRepository, IBaseQueryableRepository<Permission> queryableRepository) : base(commandRepository, queryableRepository)
    {

    }

    public async Task<Permission> CreateAsync(Permission permission)
    {
        if (!Query.Any(_ => _.Name == permission.Name))
            await CommandRepository.InsertAsync(permission);
        return permission;
    }

    public IQueryable<Permission> Permissions()
    {
        return Query;
    }
}