using System.Text.Json.Serialization;

namespace SocialApp.Wrappers.Spotify;

public record SpotifyPlaylistTrackCount( [property: JsonPropertyName( "href" )] string? Href , [property: JsonPropertyName( "total" )] int? Total );
