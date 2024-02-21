// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using LumexUI.Common;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace LumexUI;

public abstract class LumexNavBase : LumexComponentBase
{
	/// <summary>
	/// Defines the content to be rendered inside the navigation.
	/// </summary>
	[Parameter] public RenderFragment? ChildContent { get; set; }

	/// <summary>
	/// Defines the orientation of the navigation.
	/// </summary>
	/// <remarks>Default is <see cref="Orientation.Horizontal"/></remarks>
	[Parameter] public Orientation Orientation { get; set; }

	protected override void BuildRenderTree( RenderTreeBuilder builder )
	{
		builder.OpenElement( 0, "ul" );
		builder.AddAttribute( 1, "class", RootClass );
		builder.AddAttribute( 2, "style", RootStyle );
		builder.AddMultipleAttributes( 3, AdditionalAttributes );
		builder.AddContent( 4, ChildContent );
		builder.CloseElement();
	}
}
