// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using LumexUI.Common;
using LumexUI.Utilities;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace LumexUI;

public partial class LumexIconButton : LumexComponentBase
{
	/// <summary>
	/// Defines an optional icon template for the <see cref="LumexIconButton"/>.
	/// <para>You should supply either <see cref="Icon"/> or <see cref="IconTemplate"/>, but not both.</para>
	/// </summary>
	[Parameter] public RenderFragment IconTemplate { get; set; } = null!;

	/// <summary>
	/// Defines an icon for the <see cref="LumexIconButton"/>.
	/// <para>You should supply either <see cref="Icon"/> or <see cref="IconTemplate"/>, but not both.</para>
	/// </summary>
	[Parameter] public string? Icon { get; set; }

	/// <summary>
	/// Specifies the size of the <see cref="LumexIconButton"/>. This accepts one of the <see cref="Data.Size"/>.
	/// </summary>
	/// <remarks>Default value is <see cref="Size.Medium"/></remarks>
	[Parameter] public Size Size { get; set; } = Size.Medium;

	/// <summary>
	/// Specifies the viewbox of the icon to be shown.
	/// </summary>
	/// <remarks>Default value is `0 -960 960 960` (Material Symbols)</remarks>
	[Parameter] public string? ViewBox { get; set; } = "0 -960 960 960";

	/// <summary>
	/// Defines an event callback that is fired whenever the button is clicked.
	/// </summary>
	[Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }

	protected override string RootClass =>
		new CssBuilder( "lumex-btn lumex-icon-btn" )
			.AddClass( base.RootClass )
		.Build();

	private async Task HandleClickAsync( MouseEventArgs args )
	{
		await OnClick.InvokeAsync( args );
	}
}