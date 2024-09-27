// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

namespace LumexUI.Docs.Common;

internal class NavigationStore
{
    private static Navigation? _navigation;

    private static NavigationCategory GettingStartedCategory =>
        new NavigationCategory( "Getting Started", Icons.Rounded.AutoStories )
            .Add( "Overview" )
            .Add( "Installation" );

    private static NavigationCategory CustomizationCategory =>
        new NavigationCategory( "Customization", Icons.Rounded.DesignServices )
            .Add( "Theme" )
            .Add( "Layout" )
            .Add( "Colors" );

    private static NavigationCategory ComponentsCategory =>
        new NavigationCategory( "Components", Icons.Rounded.Joystick )
            .Add( nameof( LumexAccordion ) )
            .Add( nameof( LumexButton ) )
            .Add( nameof( LumexCard ) )
            .Add( nameof( LumexCheckbox ) )
            .Add( nameof( LumexCollapse ) )
            .Add( nameof( LumexDivider ) )
            .Add( nameof( LumexIcon ) )
            .Add( nameof( LumexLink ) )
            .Add( nameof( LumexNavLink ) )
            .Add( nameof( LumexNavbar ) )
            .Add( nameof( LumexNumbox<T> ) )
            .Add( nameof( LumexPopover ) )
            .Add( nameof( LumexSwitch ) )
            .Add( nameof( LumexTextbox ) )
            .Add( nameof( LumexThemeProvider ) );

    private static NavigationCategory ComponentsApiCategory =>
        new NavigationCategory( "Components API", Icons.Rounded.Manufacturing )
            .Add( nameof( LumexAccordion ) )
            .Add( nameof( LumexAccordionItem ) )
            //.Add( nameof( LumexBooleanInputBase ) )
            .Add( nameof( LumexButton ) )
            .Add( nameof( LumexCard ) )
            .Add( nameof( LumexCardBody ) )
            .Add( nameof( LumexCardFooter ) )
            .Add( nameof( LumexCardHeader ) )
            .Add( nameof( LumexCheckbox ) )
            .Add( nameof( LumexCheckboxGroup ) )
            .Add( nameof( LumexCollapse ) )
            .Add( nameof( LumexComponent ) )
            //.Add( nameof( LumexComponentBase ) )
            //.Add( nameof( LumexDebouncedInputBase<T> ) )
            .Add( nameof( LumexDivider ) )
            //.Add( nameof( LumexInputBase<T> ) )
            //.Add( nameof( LumexInputFieldBase<T> ) )
            .Add( nameof( LumexIcon ) )
            .Add( nameof( LumexLink ) )
            //.Add( nameof( LumexLinkBase ) )
            .Add( nameof( LumexNavLink ) )
            .Add( nameof( LumexNavbar ) )
            .Add( nameof( LumexNavbarBrand ) )
            .Add( nameof( LumexNavbarContent ) )
            .Add( nameof( LumexNavbarItem ) )
            .Add( nameof( LumexNavbarMenu ) )
            .Add( nameof( LumexNavbarMenuItem ) )
            .Add( nameof( LumexNavbarMenuToggle ) )
            .Add( nameof( LumexNumbox<T> ) )
            .Add( nameof( LumexPopover ) )
            .Add( nameof( LumexPopoverContent ) )
            .Add( nameof( LumexPopoverTrigger ) )
            .Add( nameof( LumexSwitch ) )
            .Add( nameof( LumexTextbox ) )
            .Add( nameof( LumexThemeProvider ) );

    public static Navigation GetNavigation()
    {
        _navigation ??= new Navigation()
            .Add( GettingStartedCategory )
            .Add( CustomizationCategory )
            .Add( ComponentsCategory )
            .Add( ComponentsApiCategory );

        return _navigation;
    }
}
