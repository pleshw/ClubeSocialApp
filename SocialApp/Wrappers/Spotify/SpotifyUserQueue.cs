using System.Text.Json.Serialization;

namespace SocialApp.Wrappers.Spotify;

public record SpotifyUserQueue( [property: JsonPropertyName( "currently_playing" )] SpotifyTrack? CurrentlyPlaying , [property: JsonPropertyName( "queue" )] List<SpotifyTrack>? Queue );
