// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using LumexUI.Utilities;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;

namespace LumexUI;

public partial class LumexLink : LumexComponentBase
{
    /// <summary>
    /// Specifies a child contents of this instance.
    /// </summary>
    [Parameter] public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Specifies a value representing the URL route to be navigated to.
    /// </summary>
    [Parameter] public string Route { get; set; } = "#";

    /// <summary>
    /// Specifies a value representing the URL matching behavior.
    /// </summary>
    /// <remarks>Default is <see cref="NavLinkMatch.All"/></remarks>
    [Parameter] public NavLinkMatch Match { get; set; } = NavLinkMatch.All;

    /// <summary>
    /// Specifies a CSS class name applied to the NavLink when the
    /// current route matches the NavLink href.
    /// </summary>
    [Parameter] public string ActiveClass { get; set; } = Constants.ComponentStates.Active;

    [CascadingParameter] private LumexNavBase Parent { get; set; } = default!;

    protected override string RootClass =>
        new CssBuilder( $"{Name}-root" )
            .AddClass( $"{GetParentName()}-link", when: Parent is not null )
            .AddClass( base.RootClass )
        .Build();

    private string GetParentName()
    {
        if( Parent is LumexNavbarContent navBarContent )
        {
            return navBarContent.Parent.Name;
        }
        else if( Parent is LumexNav navMenu )
        {
            return navMenu.Name;
        }

        return string.Empty;
    }
}