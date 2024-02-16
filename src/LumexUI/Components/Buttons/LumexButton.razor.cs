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
	/// Defines an optional content to be rendered inside the button element.
	/// </summary>
	[Parameter] public RenderFragment? ChildContent { get; set; }

	/// <summary>
	/// Defines an optional icon template for the <see cref="LumexButton"/>.
	/// <para>You should supply either <see cref="Icon"/> or <see cref="IconTemplate"/>, but not both.</para>
	/// </summary>
	[Parameter] public RenderFragment IconTemplate { get; set; } = null!;

	/// <summary>
	/// Specifies the type of the <see cref="LumexButton"/> that define the default behavior of the button.
	/// <para>Possible values are: <c>submit</c>, <c>reset</c> and <c>button</c>. See <seealso href="https://developer.mozilla.org/en-US/docs/Web/HTML/Element/button#attributes">The Button element</seealso>.</para>
	/// </summary>
	/// <remarks>Default value is <see cref="ButtonType.Button"/></remarks>
	[Parameter] public ButtonType Type { get; set; }

	/// <summary>
	/// Specifies the appearance variant of the <see cref="LumexButton"/>. This could be either filled, outlined or ghosty.
	/// </summary>
	/// <remarks>Default value is <see cref="ButtonVariant.Filled"/></remarks>
	[Parameter] public ButtonVariant Variant { get; set; }

	/// <summary>
	/// Specifies the color of the <see cref="LumexButton"/>. This accepts one of the <see cref="ThemeColor"/>.
	/// </summary>
	/// <remarks>Default value is <see cref="ThemeColor.None"/></remarks>
	[Parameter] public ThemeColor Color { get; set; }

	/// <summary>
	/// Specifies the size of the <see cref="LumexButton"/>. This accepts one of the <see cref="Data.Size"/>.
	/// </summary>
	/// <remarks>Default value is <see cref="Size.Medium"/></remarks>
	[Parameter] public Size Size { get; set; } = Size.Medium;

	/// <summary>
	/// Indicates whether the button element is disabled.
	/// </summary>
	[Parameter] public bool Disabled { get; set; }

	/// <summary>
	/// Defines an optional icon for the <see cref="LumexButton"/>.
	/// If given, the icon will be put before or after the label of the button depending on the <see cref="IconPosition"/> parameter.
	/// <para>You should supply either <see cref="Icon"/> or <see cref="IconTemplate"/>, but not both.</para>
	/// </summary>
	[Parameter] public string? Icon { get; set; }

	/// <summary>
	/// Specifies the position of an icon of the <see cref="LumexButton"/>. This accepts one of the <see cref="Position"/>.
	/// </summary>
	/// <remarks>Default value is <see cref="Position.End"/></remarks>
	[Parameter] public Position IconPosition { get; set; } = Position.End;

	/// <summary>
	/// Specifies an optional CSS class names for the icon of the <see cref="LumexButton"/>. 
	/// If given, this will be included in the class attribute of the table header and body cells for this column.
	/// </summary>
	[Parameter] public string? IconClass { get; set; }

	/// <summary>
	/// Defines an event callback that is fired whenever the button is clicked.
	/// </summary>
	[Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }

	protected override string RootClass =>
		new CssBuilder( "lumex-btn" )
			.AddClass( $"lumex-btn--{Size.ToDescription()}", when: Size != Size.Medium )
			.AddClass( $"lumex-btn--{Color.ToDescription()}", when: Filled && Color != ThemeColor.None )
			.AddClass( $"lumex-btn--{Variant.ToDescription()}-{Color.ToDescription()}", when: !Filled )
			.AddClass( base.RootClass )
		.Build();

	private string IconCssClass =>
		new CssBuilder( "lumex-btn-icon" )
			.AddClass( $"lumex-btn-icon--{IconPosition.ToDescription()}" )
		.Build();

	private bool Filled => Variant == ButtonVariant.Fill;

	private async Task HandleClickAsync( MouseEventArgs args )
	{
		if( Disabled )
		{
			return;
		}

		await OnClick.InvokeAsync( args );
	}
}