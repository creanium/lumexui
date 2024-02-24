// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using LumexUI.Docs.Data;

namespace LumexUI.Docs.Services;

internal class NavigationService : INavigationService
{
    private DocsNavigation? _navigation;

    private NavigationCategory GettingStartedCategory =>
        new NavigationCategory( "Getting Started" )
            .AddItem( "Overview" )
            .AddItem( "Installation" )
            .AddItem( "Usage" )
            .AddItem( "Support" );

    private NavigationCategory ComponentsCategory =>
        new NavigationCategory( "Components" )
            .AddGroup( new NavigationCategory( "Surfaces" )
                .AddItem( typeof( LumexNavbar ) )
                .AddItem( typeof( LumexToolbar ) ) )
            .AddGroup( new NavigationCategory( "Inputs" )
                .AddItem( typeof( LumexTextBox ) )
                .AddItem( typeof( LumexNumBox<T> ) ) )
            .AddGroup( new NavigationCategory( "Navigation" )
                .AddItem( typeof( LumexDrawer ) )
                .AddItem( typeof( LumexNavMenu ) ) );

    private NavigationCategory ComponentsApiCategory =>
        new NavigationCategory( "Components API" )
        .CopyFrom( ComponentsCategory );

    private NavigationCategory CustomizationCategory =>
        new NavigationCategory( "Customization" )
            .AddItem( "Theming" )
            .AddItem( "Palette" )
            .AddItem( "Typography" )
            .AddItem( "Breakpoints" )
            .AddItem( "Colors" )
            .AddItem( "Default theme" )
        .Lock();

    private NavigationCategory CssUtilitiesCategory =>
        new NavigationCategory( "CSS Utilities" )
            .AddGroup( new NavigationCategory( "Layout" )
                .AddItem( "Position" )
                .AddItem( "Display" )
                .AddItem( "Overflow" ) )
            .AddGroup( new NavigationCategory( "Flexbox" )
                .AddItem( "Flex" )
                .AddItem( "Flex Direction" )
                .AddItem( "Gap" )
                .AddItem( "Align Items" )
                .AddItem( "Justify Content" ) )
            .AddGroup( new NavigationCategory( "Borders" )
                .AddItem( "Border" )
                .AddItem( "Border Radius" ) )
            .AddGroup( new NavigationCategory( "Spacing" )
                .AddItem( "Padding" )
                .AddItem( "Margin" ) )
            .AddGroup( new NavigationCategory( "Typography" )
                .AddItem( "Font Size" )
                .AddItem( "Font Weight" )
                .AddItem( "Text Align" )
                .AddItem( "Text Color" )
                .AddItem( "Text Decoration" )
                .AddItem( "Text Transform" ) )
        .Lock();

    public DocsNavigation GetNavigation()
    {
        _navigation ??= new DocsNavigation()
            .AddCategory( GettingStartedCategory )
            .AddCategory( ComponentsCategory )
            .AddCategory( ComponentsApiCategory )
            .AddCategory( CustomizationCategory )
            .AddCategory( CssUtilitiesCategory );

        return _navigation;
    }
}