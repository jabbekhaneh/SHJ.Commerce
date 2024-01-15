using SHJ.ExceptionHandler;

namespace SHJ.Commerce.Domain.Aggregates.Identity;

public class PermissionManager : BaseDomainService<Permission>
{
    public PermissionManager(IBaseCommandRepository<Permission> commandRepository, IBaseQueryableRepository<Permission> queryableRepository) : base(commandRepository, queryableRepository)
    {

    }
    public async Task<Permission> Create(Permission permission)
    {
        if (Query.Any(_ => _.Name == permission.Name))
            throw new BaseBusinessException(GlobalIdentityErrors.DublicationPersissionName);

        await CommandRepository.InsertAsync(permission);
        return permission;
    }

}