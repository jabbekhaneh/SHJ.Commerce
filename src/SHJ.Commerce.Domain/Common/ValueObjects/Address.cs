namespace SHJ.Commerce.Domain.Common.ValueObjects;

public class Address : ValueObject
{
    public string Country { get; private set; }
    public string Province { get; private set; }
    public string City { get; private set; }
    public string Street { get; private set; }
    public string ZipCode { get; private set; }
    public string State { get; private set; }
    public string Latitude { get; private set; }
    public string Longitude { get; private set; }

    public Address() { }

    public Address(string country, string province, string city, string street, string zipcode, string state, string latitude, string longitude)
    {
        Street = street;
        City = city;
        State = state;
        Country = country;
        ZipCode = zipcode;
        Country = country;
        Province = province;
        Latitude = latitude;
        Longitude = longitude;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Country;
        yield return Province;
        yield return City;
        yield return Street;
        yield return ZipCode;
        yield return State;
        yield return Latitude;
        yield return Longitude;s
    }
}