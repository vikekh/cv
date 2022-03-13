using Ardalis.GuardClauses;

namespace Vikekh.Cv.WebRazor.Models;

public class Resume
{
    private readonly List<Profile> _profiles;

    public Resume(string name, string label, string email, string phone, Location location, Uri image, string? summary, Uri? url, IEnumerable<Profile>? profiles)
    {
        Guard.Against.NullOrWhiteSpace(name, nameof(name));
        Guard.Against.NullOrWhiteSpace(label, nameof(label));
        Guard.Against.NullOrWhiteSpace(email, nameof(email));
        Guard.Against.NullOrWhiteSpace(phone, nameof(phone));
        Guard.Against.Null(location, nameof(location));
        Guard.Against.Null(image, nameof(image));

        Name = name;
        Label = label;
        Email = email;
        Phone = phone;
        Location = location;
        Image = image;
        Summary = summary;
        Url = url;

        _profiles = new List<Profile>();

        if (profiles != null) _profiles.AddRange(profiles);
    }

    public string Email { get; private set; }

    public Uri Image { get; private set; }

    public string Label { get; private set; }

    public Location Location { get; private set; }

    public string Name { get; private set; }

    public string Phone { get; private set; }

    public IEnumerable<Profile> Profiles => _profiles;

    public string? Summary { get; private set; }

    public Uri? Url { get; private set; }
}
