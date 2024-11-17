using System.Text.Json.Serialization;

namespace SocialApp.Wrappers.Spotify;

public record SpotifyRestrictions( [property: JsonPropertyName( "reason" )] string? Reason );
