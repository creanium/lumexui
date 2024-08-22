using LumexUI.Docs.Client.Common;
using LumexUI.Docs.Client.Services;

namespace LumexUI.Docs.Client.Components;

public partial class NavMenu
{
    private DocsNavigation _navigation = default!;

    protected override void OnInitialized()
    {
        _navigation = Navigation.Build();
    }

    private static string GetPathSegment( string name )
    {
        if( name is "Components API" )
        {
            return "api";
        }

        return $"docs/{name}".ToLowerInvariant().Replace( " ", "-" );
    }
}
