using System.Text.Json.Serialization;

namespace SocialApp.Wrappers.Spotify;

public record SpotifyExplicitContent( [property: JsonPropertyName( "filter_enabled" )] bool Enabled , [property: JsonPropertyName( "filter_locked" )] bool Locked );
