using SHJ.BaseFramework.Shared;
using System.ComponentModel.DataAnnotations;

namespace SHJ.Commerce.ApplicationContracts.Contracts.Identity
{
    public class CreateRoleDto : BaseDto
    {
        public CreateRoleDto()
        {
            Permissions = new();
            if (Permissions.Count <= 0)
                throw new ArgumentNullException(nameof(Permissions));
        }

        [Required, MaxLength(256)]
        public string Name { get; set; } = string.Empty;
        public List<Guid> Permissions { get; set; }
    }
}