// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

namespace LumexUI.Grid.Data;

public sealed record GridRowRenderArgs<TGridItem> : GridRenderArgs<TGridItem>
{
	public GridRowRenderArgs( TGridItem item ) : base( item )
	{
	}
}
