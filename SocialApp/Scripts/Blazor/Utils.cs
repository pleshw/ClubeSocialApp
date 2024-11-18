using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace SocialApp.Scripts.Blazor;

public static class Utils
{
    public static async Task<bool> IsInitialLoad(this ComponentBase component, IJSRuntime jsRuntime )
    {
        if (component == null)
        {
            throw new ArgumentNullException( nameof( component ) , "Component cannot be null." );
        }

        if (jsRuntime == null)
        {
            throw new ArgumentNullException( nameof( jsRuntime ) , "JSRuntime cannot be null." );
        }

        bool isInitialLoad = await jsRuntime.InvokeAsync<bool>( "window.isInitialLoad" );
        if (isInitialLoad)
        {
            await jsRuntime.InvokeVoidAsync( "eval" , "window.isInitialLoad = false;" );
            return true;
        }

        return false;
    }
}