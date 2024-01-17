using System.ComponentModel.DataAnnotations.Schema;

namespace SHJ.Commerce.Domain.Aggregates.Identity;

public class Permission : BaseEntity
{
    public Permission() { }

    public Permission(string name, string? displayName = default, string? parentName = default, Guid? parentId = default)
    {
        Name = name.ToLower();
        DisplayName = displayName;
        ParentName = parentName;
        ParentId = parentId;
    }

    public virtual string Name { get; private set; } = string.Empty;
    public virtual string? DisplayName { get; private set; } = string.Empty;
    public virtual string? ParentName { get; private set; } = string.Empty;
    public virtual Guid? ParentId { get; private set; }

}
