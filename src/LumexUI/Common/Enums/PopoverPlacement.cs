﻿// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using System.ComponentModel;

namespace LumexUI.Common;

/// <summary>
/// Specifies the placement options for the <see cref="LumexPopover"/>.
/// </summary>
public enum PopoverPlacement
{
	/// <summary>
	/// Positions the popover at the top.
	/// </summary>
	[Description( "top" )]
	Top,

	/// <summary>
	/// Positions the popover at the top, aligned to the start.
	/// </summary>
	[Description( "top-start" )]
	TopStart,

	/// <summary>
	/// Positions the popover at the top, aligned to the end.
	/// </summary>
	[Description( "top-end" )]
	TopEnd,

	/// <summary>
	/// Positions the popover to the right.
	/// </summary>
	[Description( "right" )]
	Right,

	/// <summary>
	/// Positions the popover to the right, aligned to the start.
	/// </summary>
	[Description( "right-start" )]
	RightStart,

	/// <summary>
	/// Positions the popover to the right, aligned to the end.
	/// </summary>
	[Description( "right-end" )]
	RightEnd,

	/// <summary>
	/// Positions the popover at the bottom.
	/// </summary>
	[Description( "bottom" )]
	Bottom,

	/// <summary>
	/// Positions the popover at the bottom, aligned to the start.
	/// </summary>
	[Description( "bottom-start" )]
	BottomStart,

	/// <summary>
	/// Positions the popover at the bottom, aligned to the end.
	/// </summary>
	[Description( "bottom-end" )]
	BottomEnd,

	/// <summary>
	/// Positions the popover to the left.
	/// </summary>
	[Description( "left" )]
	Left,

	/// <summary>
	/// Positions the popover to the left, aligned to the start.
	/// </summary>
	[Description( "left-start" )]
	LeftStart,

	/// <summary>
	/// Positions the popover to the left, aligned to the end.
	/// </summary>
	[Description( "left-end" )]
	LeftEnd
}

