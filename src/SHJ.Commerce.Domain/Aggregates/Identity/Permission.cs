using System.ComponentModel.DataAnnotations.Schema;

namespace SHJ.Commerce.Domain.Aggregates.Identity;

public class Permission : BaseEntity<Guid>
{
    public Permission() { Permissions = new(); }

    public Permission(string name, string? displayName, string? parentName, Guid? parentId)
    {
        Name = name.ToLower();
        DisplayName = displayName;
        ParentName = parentName;
        ParentId = parentId;
        Permissions = new();
    }

    public virtual string Name { get; private set; } = string.Empty;
    public virtual string? DisplayName { get; private set; } = string.Empty;
    public virtual string? ParentName { get; private set; } = string.Empty;
    public virtual Guid? ParentId { get; private set; }

    [ForeignKey("ParentId")]
    public virtual List<Permission> Permissions { get; set; } 
}
