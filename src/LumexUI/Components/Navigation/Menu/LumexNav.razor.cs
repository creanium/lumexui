// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using LumexUI.Common;
using LumexUI.Utilities;

using Microsoft.AspNetCore.Components;

namespace LumexUI;

public partial class LumexNav : LumexNavBase, ISlotComponent<NavSlots>
{
	/// <summary>
	/// Defines the orientation of the navigation.
	/// </summary>
	/// <remarks>Default is <see cref="Orientation.Horizontal"/></remarks>
	[Parameter] public Orientation Orientation { get; set; }

    /// <summary>
    /// Defines the CSS classes for slots of the navigation.
    /// </summary>
	[Parameter] public NavSlots Slots { get; set; } = new();

	protected override string RootClass =>
		new CssBuilder( $"{Name}-root" )
			.AddClass( $"{Name}--vertical", when: Orientation is Orientation.Vertical )
			.AddClass( $"{Slots.Root}", when: !string.IsNullOrEmpty( Slots.Root ) )
			.AddClass( base.RootClass )
		.Build();
}