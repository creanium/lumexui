// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using Microsoft.AspNetCore.Components;

namespace LumexUI;

public abstract class LumexNavBase : LumexComponentBase
{
	/// <summary>
	/// Defines the content to be rendered inside the navigation.
	/// </summary>
	[Parameter] public RenderFragment? ChildContent { get; set; }
}
