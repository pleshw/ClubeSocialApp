using Microsoft.JSInterop;
using SocialApp.Components.Spotify;

namespace SocialApp.Wrappers;

public class SpotifyJSInterop
{
    public IJSObjectReference? SpotifyModule { get; private set; }
    public IJSObjectReference? SpotifyPlayer { get; private set; }

    public SpotifyJSInterop(  )
    {

    }

    public async Task<IJSObjectReference> CreateSpotifyPlayer( string accessToken , IJSObjectReference spotifyModule , string deviceName )
    {
        return await spotifyModule.InvokeAsync<IJSObjectReference>( "createSpotifyPlayer" , accessToken, deviceName );
    }

    public async Task SetSpotifyPlayerListeners( IJSObjectReference spotifyModule, IJSObjectReference spotifyPlayer )
    {
        await spotifyModule.InvokeVoidAsync( "setSpotifyPlayerListeners" , spotifyPlayer );
    }

    public async Task DisconnectSpotifyPlayer( IJSObjectReference spotifyModule , IJSObjectReference spotifyPlayer )
    {
        await spotifyModule.InvokeVoidAsync( "disconnectSpotifyPlayer" , spotifyPlayer );
    }

    public async Task ConnectSpotifyPlayer( IJSObjectReference spotifyModule , IJSObjectReference spotifyPlayer )
    {
        await spotifyModule.InvokeVoidAsync( "connectSpotifyPlayer" , spotifyPlayer);
    }


    public async Task<IJSObjectReference?> InitSpotifyPlayer( string accessToken, IJSObjectReference spotifyModule, string deviceName )
    {
        SpotifyModule = spotifyModule;
        SpotifyPlayer = await CreateSpotifyPlayer( accessToken, spotifyModule, deviceName );
        await ConnectSpotifyPlayer( spotifyModule, SpotifyPlayer );
        await SetSpotifyPlayerListeners( spotifyModule , SpotifyPlayer );
        return SpotifyPlayer;
    }
}
