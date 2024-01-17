using SHJ.BaseFramework.EntityFrameworkCore;
using SHJ.Commerce.Domain.Aggregates.Identity;
using SHJ.Commerce.Shared.Shared.Identity;
namespace SHJ.Commerce.Infrastructure.EntityFrameworkCore.Identity;

public class PermissionEFCommandRepository : BaseCommandEFRepository<ApplicationDbContext, Permission>, ICommandPermissionRepository
{
    public PermissionEFCommandRepository(ApplicationDbContext context) : base(context)
    {
    }
}
