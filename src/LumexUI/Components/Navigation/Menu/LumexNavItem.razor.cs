// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using LumexUI.Utilities;

using Microsoft.AspNetCore.Components;

namespace LumexUI;

public partial class LumexNavItem : LumexNavItemBase
{
	[CascadingParameter] private LumexNav Parent { get; set; } = default!;

	protected override string RootClass =>
		new CssBuilder( $"{Parent.Name}-item" )
			.AddClass( Parent.Slots.Item, when: !string.IsNullOrEmpty( Parent.Slots.Item ) )
			.AddClass( base.RootClass )
		.Build();

	protected override void OnInitialized()
	{
		ParentComponentNullException.ThrowIfNull( Parent, nameof( LumexNav ) );
	}
}