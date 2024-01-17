using SHJ.BaseFramework.EntityFrameworkCore;
using SHJ.Commerce.Domain.Aggregates.Identity;
using SHJ.Commerce.Shared.Shared.Identity;
namespace SHJ.Commerce.Infrastructure.EntityFrameworkCore.Identity;

public class PermissionEFQueryRepository : BaseQueryEFRepository<ApplicationDbContext, Permission>, IQueryPermissionRepository
{
    public PermissionEFQueryRepository(ApplicationDbContext context) : base(context)
    {

    }
}
