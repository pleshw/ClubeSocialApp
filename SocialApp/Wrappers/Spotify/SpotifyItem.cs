﻿using System.Text.Json.Serialization;

namespace SocialApp.Wrappers.Spotify;

public record SpotifyItem( [property: JsonPropertyName( "album" )] SpotifyAlbum? Album , [property: JsonPropertyName( "artists" )] List<SpotifyArtists>? Artists , [property: JsonPropertyName( "available_markets" )] List<string>? AvailableMarkets , [property: JsonPropertyName( "disc_number" )] int? DiscNumber , [property: JsonPropertyName( "duration_ms" )] int? DurationMs , [property: JsonPropertyName( "explicit" )] bool? Explicit , [property: JsonPropertyName( "external_ids" )] Dictionary<string , string>? ExternalIds , [property: JsonPropertyName( "external_urls" )] SpotifyExternalUrls? ExternalUrls , [property: JsonPropertyName( "href" )] string? Href , [property: JsonPropertyName( "id" )] string? Id , [property: JsonPropertyName( "is_playable" )] bool? IsPlayable , [property: JsonPropertyName( "linked_from" )] object? LinkedFrom , [property: JsonPropertyName( "restrictions" )] SpotifyRestrictions? Restrictions , [property: JsonPropertyName( "name" )] string? Name , [property: JsonPropertyName( "popularity" )] int? Popularity , [property: JsonPropertyName( "preview_url" )] string? PreviewUrl , [property: JsonPropertyName( "track_number" )] int? TrackNumber , [property: JsonPropertyName( "type" )] string? Type , [property: JsonPropertyName( "uri" )] string? Uri , [property: JsonPropertyName( "is_local" )] bool? IsLocal );
