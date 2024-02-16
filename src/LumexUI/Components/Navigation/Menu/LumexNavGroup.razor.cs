// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using LumexUI.Common;
using LumexUI.Utilities;

using Microsoft.AspNetCore.Components;

namespace LumexUI;

public partial class LumexNavGroup : LumexComponentBase, ISlotComponent<NavGroupSlots>
{
	/// <summary>
	/// Defines the content to be rendered inside the navigation group.
	/// </summary>
	[Parameter] public RenderFragment? ChildContent { get; set; }

	/// <summary>
	/// Specifies the title text for the navigation group.
	/// </summary>
	[Parameter] public string? Title { get; set; }

	/// <summary>
	/// Indicates whether the navigation group can be expanded.
	/// </summary>
	/// <remarks>Default value is <see langword="true"/></remarks>
	[Parameter] public bool Expandable { get; set; } = true;

	/// <summary>
	///
	/// </summary>
	[Parameter] public NavGroupSlots Slots { get; set; } = new();

	protected override string RootClass =>
		new CssBuilder( "lumex-nav-group" )
			.AddClass( "lumex-nav-group--expandable", when: Expandable )
			.AddClass( Constants.ComponentStates.Expanded, when: _expanded )
			.AddClass( Slots.Root, when: !string.IsNullOrEmpty( Slots.Root ) )
			.AddClass( base.RootClass )
		.Build();

	private string ContentCssClass =>
		new CssBuilder( "lumex-nav-group-content" )
			.AddClass( Slots.Content, when: !string.IsNullOrEmpty( Slots.Content ) )
		.Build();

	private bool _expanded;

	private void ToggleGroupExpansion()
	{
		if( Expandable )
		{
			_expanded = !_expanded;
		}
	}
}