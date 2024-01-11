namespace SHJ.Commerce.Domain.Common.ValueObjectCollections;

public sealed class Name : ValueObject
{
    public Name()
    {
        
    }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string MiddaleName { get; set; } = string.Empty;
    protected override IEnumerable<object> GetEqualityComponents()
    {
        throw new NotImplementedException();
    }
}
