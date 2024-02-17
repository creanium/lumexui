// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using LumexUI.Common;
using LumexUI.Utilities;

using Microsoft.AspNetCore.Components;

namespace LumexUI;
public partial class LumexNavbar : LumexComponentBase, ISlotComponent<NavbarSlots>
{
    /// <summary>
    /// Defines the content to be rendered inside the navbar.
    /// </summary>
    [Parameter] public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Defines the content to be rendered inside the navbar for branding.
    /// </summary>
    [Parameter] public RenderFragment? BrandContent { get; set; }

    /// <summary>
    /// Indicates whether the navbar will be sticked to top of the page.
    /// </summary>
    /// <remarks>Default value is <see langword="false"/></remarks>
    [Parameter] public bool Sticky { get; set; }

    /// <summary>
    /// Defines the breakpoint until which the <see cref="LumexNavbar"/> will be full-width.
    /// </summary>
    /// <remarks>Default value is <see cref="Breakpoint.Xs"/></remarks>
    [Parameter] public Breakpoint Breakpoint { get; set; }

    /// <summary>
    /// Defines the slots of the navbar.
    /// </summary>
    [Parameter] public NavbarSlots Slots { get; set; } = new();

    protected override string RootClass =>
        new CssBuilder( $"{ComponentName}-root" )
            .AddClass( $"{ComponentName}--sticky", when: Sticky )
            .AddClass( $"{Slots.Root}", when: !string.IsNullOrEmpty( Slots.Root ) )
            .AddClass( base.RootClass )
        .Build();

    private string BrandClass =>
        new CssBuilder( $"{ComponentName}-brand" )
            .AddClass( $"{Slots.Brand}", when: !string.IsNullOrEmpty( Slots.Brand ) )
        .Build();

    private string ContentClass =>
        new CssBuilder( $"{ComponentName}-content" )
            .AddClass( $"{Slots.Content}", when: !string.IsNullOrEmpty( Slots.Content ) )
        .Build();
}