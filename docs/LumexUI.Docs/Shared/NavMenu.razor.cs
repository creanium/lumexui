// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using LumexUI.Docs.Data;
using LumexUI.Docs.Services;

using Microsoft.AspNetCore.Components;

namespace LumexUI.Docs;

public partial class NavMenu
{
	[Inject] private INavigationService NavigationService { get; set; } = default!;

    private readonly RenderFragment _renderNavigation;
    private DocsNavigation _navigation;
    private NavGroupSlots _navGroupSlots;

    public NavMenu()
    {
        _navigation = new();
        _navGroupSlots = new()
        {
            Wrapper = "ps-1.5",
            Content = "gap-2 mt-3 mb-1"
        };

        _renderNavigation = RenderNavigation;
    }

    protected override void OnInitialized()
    {
        _navigation = NavigationService.GetNavigation();
    }

    private string BuildPathSegment( string name )
    {
        if( name == "Components API" )
        {
            return "api";
        }

        return $"docs/{name}".ToLowerInvariant().Replace( " ", "-" );
    }
}
