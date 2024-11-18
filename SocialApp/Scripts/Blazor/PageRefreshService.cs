using Microsoft.JSInterop;
using System;


namespace SocialApp.Scripts.Blazor;


public class PageRefreshService( IJSRuntime jsRuntime )
{
    private readonly IJSRuntime _jsRuntime = jsRuntime;

    public event EventHandler<bool>? OnPageRefresh;

    public async Task CheckPageRefreshAsync()
    {
        bool isInitialLoad = await _jsRuntime.InvokeAsync<bool>( "eval" , "window.isInitialLoad" );

        if (isInitialLoad)
        {
            await _jsRuntime.InvokeVoidAsync( "eval" , "window.isInitialLoad = false;" );
            OnPageRefresh?.Invoke( this , true );
        }
        else
        {
            OnPageRefresh?.Invoke( this , false );
        }
    }
}