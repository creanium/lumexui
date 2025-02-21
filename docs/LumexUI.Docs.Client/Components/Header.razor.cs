// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using Microsoft.AspNetCore.Components;

namespace LumexUI.Docs.Client.Components;

public partial class Header
{
	private readonly RenderFragment _renderHeadeActions;

	private readonly NavItem[] _navItems =
	[
		new("/", "Docs"),
		new("/docs/components", "Components"),
	];

	private readonly NavbarSlots _navbarClasses = new()
	{
		Wrapper = "py-5 gap-6"
	};

	public Header()
	{
		_renderHeadeActions = RenderHeaderActions;
	}

	private record NavItem( string Link, string Name );
}