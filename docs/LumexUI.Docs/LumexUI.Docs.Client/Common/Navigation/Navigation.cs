// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using LumexUI.Docs.Client.Common;

namespace LumexUI.Docs.Client.Services;

internal class Navigation
{
    private static DocsNavigation? _navigation;

    private static NavigationCategory GettingStartedCategory =>
        new NavigationCategory( "Getting Started" )
            .AddItem( "Overview" )
            .AddItem( "Installation" );

    private static NavigationCategory CustomizationCategory =>
        new NavigationCategory( "Customization" )
            .AddItem( "Theme" )
            .AddItem( "Layout" )
            .AddItem( "Colors" );

    private static NavigationCategory ComponentsCategory =>
        new NavigationCategory( "Components" )
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
            .AddItem( typeof( LumexNumbox<T> ) )
            .AddItem( typeof( LumexPopover ) )
            .AddItem( typeof( LumexSwitch ) )
            .AddItem( typeof( LumexTextbox ) )
            .AddItem( typeof( LumexThemeProvider ) );

    private static NavigationCategory ComponentsApiCategory =>
        new NavigationCategory( "Components API" )
            .CopyFrom( ComponentsCategory );

    public static DocsNavigation Build()
    {
        _navigation ??= new DocsNavigation()
            .AddCategory( GettingStartedCategory )
            .AddCategory( CustomizationCategory )
            .AddCategory( ComponentsCategory )
            .AddCategory( ComponentsApiCategory );

        return _navigation;
    }
}
