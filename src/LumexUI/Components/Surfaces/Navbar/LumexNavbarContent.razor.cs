// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using LumexUI.Common;
using LumexUI.Utilities;

using Microsoft.AspNetCore.Components;

namespace LumexUI;

public partial class LumexNavbarContent : LumexNavBase
{
	[CascadingParameter] private LumexNavbar Parent { get; set; } = default!;

	protected override string RootClass =>
		new CssBuilder( $"{Parent.Name}-content" )
			.AddClass( $"{Parent.Name}--vertical", when: Orientation is Orientation.Vertical )
			.AddClass( $"{Parent.Slots.Content}", when: !string.IsNullOrEmpty( Parent.Slots.Content ) )
			.AddClass( base.RootClass )
		.Build();

	protected override void OnInitialized()
	{
		ParentComponentNullException.ThrowIfNull( Parent, nameof( LumexNavbar ) );
	}
}