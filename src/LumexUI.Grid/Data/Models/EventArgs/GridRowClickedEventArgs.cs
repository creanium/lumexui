// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using Microsoft.AspNetCore.Components.Web;

namespace LumexUI.Grid.Data;

public class GridRowClickedEventArgs<TGridItem> : EventArgs
{
	public TGridItem Item { get; set; }

	public int RowIndex { get; set; }

	public MouseEventArgs? MouseEventArgs { get; set; }

	public GridRowClickedEventArgs( MouseEventArgs args, TGridItem item, int index )
	{
		MouseEventArgs = args;
		Item = item;
		RowIndex = index;
	}
}
