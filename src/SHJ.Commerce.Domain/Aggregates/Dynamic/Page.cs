namespace SHJ.Commerce.Domain.Aggregates.Dynamic;

public class Page : BaseEntity<int>
{
    public Page()
    {
        
    }
    public Guid? TenantId { get;  set; }
    public string Title { get;  set; } = string.Empty;
    public string Content { get;  set; } = string.Empty;
    public string? Script { get;  set; } = string.Empty;
    public string? Style { get;  set; } = string.Empty;
    public bool IsHomePage { get;  set; }

}
