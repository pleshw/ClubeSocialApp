using System.Text.Json.Serialization;

namespace SocialApp.Wrappers.Spotify;

public record SpotifyAvailableDevices( [property: JsonPropertyName( "devices" )] SpotifyDevice[]? Devices );
