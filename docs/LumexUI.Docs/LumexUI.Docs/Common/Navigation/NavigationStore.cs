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
        new NavigationCategory( "Customization", Icons.Rounded.Brush )
            .AddItem( "Theme" )
            .AddItem( "Layout" )
            .AddItem( "Colors" );

    private static NavigationCategory ComponentsCategory =>
        new NavigationCategory( "Components", Icons.Rounded.Joystick )
            .AddItem( typeof( LumexAccordion ) )
            .AddItem( typeof( LumexButton ) )
            .AddItem( typeof( LumexCard ) )
            .AddItem( typeof( LumexCheckbox ) )
            .AddItem( typeof( LumexCollapse ) )
            .AddItem( typeof( LumexDivider ) )
            .AddItem( typeof( LumexIcon ) )
            .AddItem( typeof( LumexLink ) )
            .AddItem( typeof( LumexNavLink ) )
            .AddItem( typeof( LumexNavbar ) )
            .AddItem( typeof( LumexNumbox<int> ) )
            .AddItem( typeof( LumexPopover ) )
            .AddItem( typeof( LumexSwitch ) )
            .AddItem( typeof( LumexTextbox ) )
            .AddItem( typeof( LumexThemeProvider ) );

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
