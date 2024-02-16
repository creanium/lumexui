// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using System.ComponentModel;

namespace LumexUI.Common;

public enum ThemeColor
{
	[Description( "none" )]
	None,

	[Description( "primary" )]
	Primary,

	[Description( "secondary" )]
	Secondary,

	[Description( "tertiary" )]
	Tertiary,

	[Description( "success" )]
	Success,

	[Description( "warning" )]
	Warning,

	[Description( "danger" )]
	Danger,

	[Description( "info" )]
	Info,

	[Description( "light" )]
	Light,

	[Description( "dark" )]
	Dark
}
