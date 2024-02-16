// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

namespace LumexUI.Grid.Data;

public sealed record GridCellRenderArgs<TGridItem> : GridRenderArgs<TGridItem>
{
	public object? Value { get; set; }

	public GridCellRenderArgs( TGridItem item, object? value ) : base( item )
	{
		Value = value;
	}
}
