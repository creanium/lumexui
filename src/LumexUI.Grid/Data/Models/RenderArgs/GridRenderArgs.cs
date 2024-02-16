// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

namespace LumexUI.Grid.Data;

public record GridRenderArgs<TGridItem>
{
	public TGridItem Item { get; set; }

	public string? Class { get; set; }

	public GridRenderArgs( TGridItem item )
	{
		Item = item;
	}
}
