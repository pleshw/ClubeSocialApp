using System.Text.Json.Serialization;

namespace SocialApp.Wrappers.Spotify;

public record SpotifyUserPlaylistsQuery( [property: JsonPropertyName( "href" )] string? QueryURL , [property: JsonPropertyName( "limit" )] int? Limit , [property: JsonPropertyName( "next" )] string? URLNextPage , [property: JsonPropertyName( "previous" )] string? UrlPreviousPage , [property: JsonPropertyName( "offset" )] int? Offset , [property: JsonPropertyName( "total" )] int? TotalAvailable , [property: JsonPropertyName( "items" )] List<SpotifySimplifiedPlaylist>? Items );
