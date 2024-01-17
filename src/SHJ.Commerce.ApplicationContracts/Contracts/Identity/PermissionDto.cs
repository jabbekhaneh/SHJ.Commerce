using SHJ.BaseFramework.Shared;

namespace SHJ.Commerce.ApplicationContracts.Contracts.Identity;

public class PermissionDto : BaseDto<Guid>
{
    public string Name { get; set; } = string.Empty;
    public string? DisplayName { get; set; } = string.Empty;
    public string? ParentName { get; set; } = string.Empty;
    public Guid? ParentId { get; set; }
}