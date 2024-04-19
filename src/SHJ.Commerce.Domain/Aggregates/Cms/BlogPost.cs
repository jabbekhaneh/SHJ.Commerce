namespace SHJ.Commerce.Domain.Aggregates.Cms;

public class BlogPost : BaseEntity<Guid>
{
    public BlogPost()
    {
        
    }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Tags { get; set; } = string.Empty;
}

