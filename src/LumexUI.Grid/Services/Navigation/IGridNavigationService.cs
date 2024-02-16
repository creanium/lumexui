// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using Microsoft.AspNetCore.Components.Web;

namespace LumexUI.Grid.Services;

internal interface IGridNavigationService
{
	ValueTask HandleKeyAsync( KeyboardEventArgs args, object? obj, bool onKeyUp );

	void RegisterAction( string key, Func<object?, ValueTask> action, bool onKeyUp );
}