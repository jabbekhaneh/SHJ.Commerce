namespace SHJ.Commerce.Domain.Common.ValueObjectCollections;

/// <summary>
/// 
/// </summary>
public sealed class Address : ValueObject
{
    public string Title { get; private set; }
    public string Country { get; private set; }
    public string Province { get; private set; }
    public string City { get; private set; }
    public string Street { get; private set; }
    public string ZipCode { get; private set; }
    public string Latitude { get; private set; }
    public string Longitude { get; private set; }

    public Address() { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="title">se</param>
    /// <param name="country"></param>
    /// <param name="province"></param>
    /// <param name="city"></param>
    /// <param name="street"></param>
    /// <param name="zipcode"></param>
    /// <param name="state"></param>
    /// <param name="latitude"></param>
    /// <param name="longitude"></param>
    public Address(string title,string country, string province, string city, string street, string zipcode, string latitude, string longitude)
    {
        Title = title;
        Street = street;
        City = city;
        Country = country;
        ZipCode = zipcode;
        Country = country;
        Province = province;
        Latitude = latitude;
        Longitude = longitude;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Title;
        yield return Country;
        yield return Province;
        yield return City;
        yield return Street;
        yield return ZipCode;
        yield return Latitude;
        yield return Longitude;
    }
}