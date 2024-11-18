using Microsoft.JSInterop;

namespace SocialApp.Scripts.Spotify;

public class SpotifyInteropService( IJSRuntime jsRuntime, SpotifyStateService stateService )
{
    private readonly IJSRuntime _jsRuntime = jsRuntime;
    public readonly SpotifyStateService StateService = stateService;

    public IJSObjectReference? SpotifyModule { get; set; }

    public async Task<IJSObjectReference> GetSpotifyModule()
    {
        return await _jsRuntime.InvokeAsync<IJSObjectReference>( "import" , "./js/spotify.js" );
    }

    public async Task InitializePlayer( string accessToken , string deviceName )
    {
         SpotifyModule ??= await GetSpotifyModule();

        if (StateService.SpotifyPlayerObject is null)
        {
            StateService.SpotifyPlayerObject = await SpotifyModule.InvokeAsync<IJSObjectReference>( "createSpotifyPlayer" , accessToken , deviceName );
            await SpotifyModule.InvokeVoidAsync( "setSpotifyPlayerListeners" , StateService.SpotifyPlayerObject );
            await SpotifyModule.InvokeVoidAsync( "connectSpotifyPlayer" , StateService.SpotifyPlayerObject );
        }
    }

    public async Task DisconnectPlayer()
    {
        SpotifyModule ??= await GetSpotifyModule();
        await SpotifyModule.InvokeVoidAsync( "disconnectSpotifyPlayer" , StateService.SpotifyPlayerObject );
    }

    public async Task Play( string deviceName )
    {
        SpotifyModule ??= await GetSpotifyModule();
        await SpotifyModule.InvokeVoidAsync( "play" , StateService.SpotifyPlayerObject , deviceName );
    }

    public async Task Reset()
    {
        await DisconnectPlayer();
        if (SpotifyModule is not null)
        {
            await SpotifyModule.DisposeAsync();
        }

        if (StateService.SpotifyPlayerObject is not null)
        {
            await StateService.SpotifyPlayerObject.DisposeAsync();
        }
    }
}