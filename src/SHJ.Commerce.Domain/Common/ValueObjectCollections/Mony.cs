
namespace SHJ.Commerce.Domain.Common.ValueObjectCollections;

public sealed class Mony : ValueObject
{
    public decimal Value { get; private set; }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        throw new NotImplementedException();
    }
}
