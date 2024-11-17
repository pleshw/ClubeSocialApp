using Microsoft.JSInterop;

namespace SocialApp.Scripts.Spotify;

public class SpotifyInteropService( IJSRuntime jsRuntime, SpotifyStateService stateService )
{
    private readonly IJSRuntime _jsRuntime = jsRuntime;
    private readonly SpotifyStateService StateService = stateService;

    public IJSObjectReference? SpotifyModule { get; private set; }

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

    public async Task Play( string deviceName )
    {
        SpotifyModule ??= await GetSpotifyModule();
        await SpotifyModule.InvokeVoidAsync( "play" , StateService.SpotifyPlayerObject , deviceName );
    }
}