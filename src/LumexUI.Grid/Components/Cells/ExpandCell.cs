// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

namespace LumexUI.Grid;

internal class ExpandCell<TGridItem> : CellBase<TGridItem>
{
	internal Action ExpandRow { get; }

	internal LumexGridRow<TGridItem> Row { get; set; }

	internal bool Expanded => Row.Expanded;

	public ExpandCell( ExpandColumn<TGridItem> column, TGridItem item ) : base( column, item )
	{
		Row = new();

		ExpandRow = () => Row.ExpandRow();
	}
}
