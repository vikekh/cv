using Ardalis.GuardClauses;

namespace Vikekh.Cv.WebRazor.Models;

public class Location
{
    public Location(string address, string postalCode, string city, string? region, string? countryCode)
    {
        Guard.Against.NullOrWhiteSpace(address, nameof(address));
        Guard.Against.NullOrWhiteSpace(postalCode, nameof(postalCode));
        Guard.Against.NullOrWhiteSpace(city, nameof(city));

        Address = address;
        PostalCode = postalCode;
        City = city;
        Region = region;
        CountryCode = countryCode;
    }

    public string Address { get; private set; }

    public string City { get; private set; }

    public string? CountryCode { get; private set; }

    public string PostalCode { get; private set; }

    public string? Region { get; private set; }
}
