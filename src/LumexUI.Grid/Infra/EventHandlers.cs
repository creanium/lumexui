// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using Microsoft.AspNetCore.Components;

namespace LumexUI.Grid.Infra;

/// <summary>
/// Configures event handlers for <see cref="LumexGrid{TGridItem}"/>.
/// </summary>
[EventHandler( "onclickoutside", typeof( EventArgs ), enableStopPropagation: true, enablePreventDefault: true )]
public static class EventHandlers
{
}
