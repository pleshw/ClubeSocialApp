using System.Text.Json.Serialization;

namespace SocialApp.Wrappers.Spotify;

public record SpotifyExternalUrls( [property: JsonPropertyName( "spotify" )] string? Spotify );
