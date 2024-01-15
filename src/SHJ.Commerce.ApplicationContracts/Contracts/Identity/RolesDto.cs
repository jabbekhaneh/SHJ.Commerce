using SHJ.BaseFramework.Shared;

namespace SHJ.Commerce.ApplicationContracts.Contracts.Identity
{
    public class RolesDto : BaseDto
    {
        public RolesDto()
        {
            Roles = new();
        }
        public List<RoleDto> Roles { get; set; }
        public int PageSize { get; set; }
    }
}