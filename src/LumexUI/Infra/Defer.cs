// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace LumexUI.Infra;

/// <summary>
/// This is used by some of the components to move theirs body rendering to the end of the render queue
/// so we can collect the list of child components first.
/// </summary>
/// <remarks>For internal use only. Do not use!</remarks>
public class Defer : ComponentBase
{
	/// <summary>
	/// For internal use only. Do not use.
	/// </summary>
	[Parameter] public RenderFragment? ChildContent { get; set; }

	/// <summary>
	/// For internal use only. Do not use.
	/// </summary>
	protected override void BuildRenderTree( RenderTreeBuilder builder )
	{
		builder.AddContent( 0, ChildContent );
	}
}
