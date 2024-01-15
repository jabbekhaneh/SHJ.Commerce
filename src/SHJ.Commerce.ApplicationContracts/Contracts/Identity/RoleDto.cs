using SHJ.BaseFramework.Shared;

namespace SHJ.Commerce.ApplicationContracts.Contracts.Identity
{
    public class RoleDto : BaseDto<Guid>
    {
        public string Name { get; set; }
    }
}