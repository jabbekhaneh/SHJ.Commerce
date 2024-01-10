namespace SHJ.Commerce.Domain.Aggregates.Identity;

public class Permission : BaseEntity<Guid>
{
    public Permission() { }
    
    
    public virtual string Name { get; set; } = string.Empty;
    public virtual string? DisplayName { get; set; } = string.Empty;
    public virtual string? ParentName { get; set; } = string.Empty;
    public virtual Guid? ParentId { get; set; }
}
