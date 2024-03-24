// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using LumexUI.Common;
using LumexUI.Utilities;

using Microsoft.AspNetCore.Components;

namespace LumexUI;

public partial class LumexIcon
{
	/// <summary>
	/// Specifies the icon to be shown. It accepts an SVG <c>path</c> of the icon.
	/// <para>You should supply either <see cref="Icon"/> or <see cref="IconTemplate"/>, but not both.</para>
	/// </summary>
	[Parameter] public string? Icon { get; set; }

	/// <summary>
	/// Specifies the viewbox of the icon to be shown.
	/// </summary>
	/// <remarks>Default value is `0 -960 960 960` (Material Symbols)</remarks>
	[Parameter] public string? ViewBox { get; set; } = "0 -960 960 960";

	/// <summary>
	/// Specifies the size of the <see cref="LumexIcon"/>. This accepts one of the <see cref="Data.Size"/>.
	/// </summary>
	/// <remarks>Default value is <see cref="Size.Medium"/></remarks>
	[Parameter] public Size Size { get; set; } = Size.Medium;

	protected override string RootClass =>
		new CssBuilder( "lumex-icon" )
			.AddClass( $"lumex-icon--{Size.ToDescription()}", when: Size != Size.Medium )
			.AddClass( base.RootClass )
		.Build();

	protected override string RootStyle =>
		new StyleBuilder()
			.AddStyle( "fill", "currentColor", when: IsSvgIcon )
			.AddStyle( base.RootStyle )
		.Build();

	private bool IsSvgIcon => !string.IsNullOrEmpty( Icon ) && Icon.StartsWith( "<" );
}