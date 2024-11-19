using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Security.Claims;

namespace SocialApp.Scripts.Spotify;

public class SpotifyInteropService( IJSRuntime jsRuntime, SpotifyStateService stateService, AuthenticationStateProvider authenticationStateProvider )
{
    private readonly IJSRuntime _jsRuntime = jsRuntime;
    public readonly SpotifyStateService StateService = stateService;
    public readonly AuthenticationStateProvider AuthenticationStateProvider = authenticationStateProvider;
    

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
        if(StateService.SpotifyPlayerObject is null)
        {
            return;
        }

        SpotifyModule ??= await GetSpotifyModule();
        await SpotifyModule.InvokeVoidAsync( "disconnectSpotifyPlayer" , StateService.SpotifyPlayerObject );
    }

    public async Task Play( string deviceName )
    {
        SpotifyModule ??= await GetSpotifyModule();
        await SpotifyModule.InvokeVoidAsync( "play" , StateService.SpotifyPlayerObject , deviceName );
    }

    public async Task ResetSpotifyClaims()
    {
        AuthenticationState authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
        ClaimsIdentity userIdentityClaims = authState.User.Identity as ClaimsIdentity ?? throw new Exception( "No user claims detected" );
        
        List<Claim> spotifyClaims = userIdentityClaims
            .Claims
            .Where( claim => claim.Type is not null && claim.Type.Contains( "spotify" , StringComparison.CurrentCultureIgnoreCase ) ).ToList();
        
        spotifyClaims.ForEach( claim => userIdentityClaims.RemoveClaim( claim ) );
    }

    public async Task Reset()
    {
        if (SpotifyModule is not null)
        {
            await SpotifyModule.DisposeAsync();
        }

        if (StateService.SpotifyPlayerObject is not null)
        {
            await StateService.SpotifyPlayerObject.DisposeAsync();
            StateService.SpotifyPlayerObject = null;
        }
    }
}