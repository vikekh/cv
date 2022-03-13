using Ardalis.GuardClauses;

namespace Vikekh.Cv.WebRazor.Models;

public class Profile
{
    public Profile(string network, Uri url, string username)
    {
        Guard.Against.NullOrWhiteSpace(network, nameof(network));
        Guard.Against.Null(url, nameof(url));
        Guard.Against.NullOrWhiteSpace(username, nameof(username));

        Network = network;
        Url = url;
        Username = username;
    }

    public string Network { get; private set; }

    public Uri Url { get; private set; }

    public string Username { get; private set; }
}
