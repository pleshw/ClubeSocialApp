using System.Text.Json.Serialization;

namespace SocialApp.Wrappers.Spotify;

public record SpotifyRequestTokenReponse( [property: JsonPropertyName( "access_token" )] string? AccessToken , [property: JsonPropertyName( "token_type" )] string? TokenType,
    [property: JsonPropertyName( "expires_in" )] int? ExpiresIn );
