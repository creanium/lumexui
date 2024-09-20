// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

namespace LumexUI.Docs.Common;

internal class NavigationStore
{
    private static Navigation? _navigation;

    private static NavigationCategory GettingStartedCategory =>
        new NavigationCategory( "Getting Started", Icons.Rounded.AutoStories )
            .AddItem( "Overview" )
            .AddItem( "Installation" );

    private static NavigationCategory CustomizationCategory =>
        new NavigationCategory( "Customization", Icons.Rounded.DesignServices )
            .AddItem( "Theme" )
            .AddItem( "Layout" )
            .AddItem( "Colors" );

    private static NavigationCategory ComponentsCategory =>
        new NavigationCategory( "Components", Icons.Rounded.Joystick )
            .AddItem( nameof( LumexAccordion ) )
            .AddItem( nameof( LumexButton ) )
            .AddItem( nameof( LumexCard ) )
            .AddItem( nameof( LumexCheckbox ) )
            .AddItem( nameof( LumexCollapse ) )
            .AddItem( nameof( LumexDivider ) )
            .AddItem( nameof( LumexIcon ) )
            .AddItem( nameof( LumexLink ) )
            .AddItem( nameof( LumexNavLink ) )
            .AddItem( nameof( LumexNavbar ) )
            .AddItem( nameof( LumexNumbox<int> ) )
            .AddItem( nameof( LumexPopover ) )
            .AddItem( nameof( LumexSwitch ) )
            .AddItem( nameof( LumexTextbox ) )
            .AddItem( nameof( LumexThemeProvider ) );

    private static NavigationCategory ComponentsApiCategory =>
        new NavigationCategory( "Components API", Icons.Rounded.Manufacturing )
            .CopyFrom( ComponentsCategory );

    public static Navigation GetNavigation()
    {
        _navigation ??= new Navigation()
            .AddCategory( GettingStartedCategory )
            .AddCategory( CustomizationCategory )
            .AddCategory( ComponentsCategory )
            .AddCategory( ComponentsApiCategory );

        return _navigation;
    }
}
