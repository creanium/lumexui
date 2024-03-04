// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using LumexUI.Common;
using LumexUI.Utilities;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace LumexUI;

public partial class LumexButton : LumexComponentBase
{
	/// <summary>
	/// Defines a content to be rendered inside the button.
	/// </summary>
	[Parameter] public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Defines a type of the button's behavior.
    /// </summary>
    /// <remarks>Default value is <see cref="ButtonType.Button"/></remarks>
    [Parameter] public ButtonType Type { get; set; }

    /// <summary>
    /// Defines a variant of the button.
    /// </summary>
    /// <remarks>Default value is <see cref="ButtonVariant.Solid"/></remarks>
    [Parameter] public ButtonVariant Variant { get; set; }

    /// <summary>
    /// Defines a color of the button.
    /// </summary>
    /// <remarks>Default value is <see cref="ThemeColor.Default"/></remarks>
    [Parameter] public ThemeColor Color { get; set; } = ThemeColor.Default;

    /// <summary>
    /// Defines a size of the button.
    /// </summary>
    /// <remarks>Default value is <see cref="Size.Medium"/></remarks>
    [Parameter] public Size Size { get; set; } = Size.Medium;

    /// <summary>
    /// Defines an optional start icon for the button.
    /// If given, the icon will be put before the label of the button.
    /// </summary>
    [Parameter] public string? StartIcon { get; set; }

	/// <summary>
	/// Defines an optional end icon for the button.
	/// If given, the icon will be put after the label of the button.
	/// </summary>
	[Parameter] public string? EndIcon { get; set; }

    /// <summary>
    /// Indicates whether the button should have the same width and height.
    /// </summary>
	[Parameter] public bool IconOnly { get; set; }

    /// <summary>
    /// Indicates whether the button is disabled.
    /// </summary>
    [Parameter] public bool Disabled { get; set; }

    /// <summary>
    /// Defines an event callback that is fired whenever the button is clicked.
    /// </summary>
    [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }

    protected override string RootClass =>
    new CssBuilder( $"{Name}-root" )
        .AddClass( $"{Name}--icon", when: IconOnly )
        .AddClass( $"{Name}--{Size.ToDescription()}", when: Size is not Size.Medium )
        .AddClass( $"{Name}--{Variant.ToDescription()}-{Color.ToDescription()}", when: Color is not ThemeColor.None )
        .AddClass( Constants.ComponentStates.Disabled, when: Disabled )
        .AddClass( base.RootClass )
    .Build();

    protected virtual Task HandleClickAsync( MouseEventArgs args )
    {
        if( Disabled )
        {
            return Task.CompletedTask;
        }

        return OnClick.InvokeAsync( args );
    }
}