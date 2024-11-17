using Microsoft.JSInterop;

namespace SocialApp.Scripts.Spotify;


public class SpotifyStateService
{
    public IJSObjectReference? SpotifyPlayerObject { get; set; }
    public string? SpotifyDeviceId { get; set; }
}

