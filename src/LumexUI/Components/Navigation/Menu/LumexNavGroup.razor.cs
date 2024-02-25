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
    /// Defines the CSS classes for slots of the navigation group.
    /// </summary>
	[Parameter] public NavGroupSlots Slots { get; set; } = new();

	[CascadingParameter] private LumexNav Parent { get; set; } = default!;

	protected override string RootClass =>
		new CssBuilder( $"{Parent.Name}-group" )
			.AddClass( Constants.ComponentStates.Expanded, when: _expanded )
			.AddClass( Slots.Root, when: !string.IsNullOrEmpty( Slots.Root ) )
			.AddClass( Parent.Slots.Group, when: !string.IsNullOrEmpty( Parent.Slots.Group ) )
			.AddClass( base.RootClass )
		.Build();

    private string LinkCssClass =>
		new CssBuilder( $"{Parent.Name}-group-link" )
            .AddClass( Slots.Title, when: !string.IsNullOrEmpty( Slots.Title ) )
        .Build();

    private string IconCssClass =>
		new CssBuilder( $"{Parent.Name}-group-icon" )
			.AddClass( Slots.Icon, when: !string.IsNullOrEmpty( Slots.Icon ) )
        .Build();

    private string WrapperCssClass =>
		new CssBuilder( $"{Parent.Name}-group-wrapper" )
			.AddClass( Slots.Wrapper, when: !string.IsNullOrEmpty( Slots.Wrapper ) )
		.Build();

    private string ContentCssClass =>
		new CssBuilder( $"{Parent.Name}-group-content" )
            .AddClass( Slots.Content, when: !string.IsNullOrEmpty( Slots.Content ) )
		.Build();

	private bool _expanded;

    protected override void OnInitialized()
    {
        ParentComponentNullException.ThrowIfNull( Parent, nameof( LumexNav ) );
    }

    private void ToggleGroupExpansion()
	{
		if( Expandable )
		{
			_expanded = !_expanded;
		}
	}
}