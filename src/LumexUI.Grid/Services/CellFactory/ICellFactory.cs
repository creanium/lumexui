// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

namespace LumexUI.Grid.Services;

public interface ICellFactory
{
	CellBase<TGridItem> Create<TGridItem>( ColumnBase<TGridItem> column, TGridItem item );
	CellBase<TGridItem> Create<TGridItem, TProp>( ColumnBase<TGridItem> column, TGridItem item );
}