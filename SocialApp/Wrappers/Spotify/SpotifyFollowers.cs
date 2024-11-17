using System.Text.Json.Serialization;

namespace SocialApp.Wrappers.Spotify;

public record SpotifyFollowers( [property: JsonPropertyName( "href" )] string? Href , [property: JsonPropertyName( "total" )] int? Total );
