using LumexUI.Docs.Client.Common;
using LumexUI.Docs.Client.Services;

namespace LumexUI.Docs.Client.Components;

public partial class NavMenu
{
    private Navigation _navigation = default!;

    protected override void OnInitialized()
    {
        _navigation = NavigationStore.GetNavigation();
    }

    private static string GetCategoryPathSegment( string name )
    {
        return name.ToLowerInvariant().Replace( " ", "-" );
    }
}
