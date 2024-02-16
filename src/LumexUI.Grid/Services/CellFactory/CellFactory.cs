// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

namespace LumexUI.Grid.Services;

public class CellFactory : ICellFactory
{
	public CellBase<TGridItem> Create<TGridItem>( ColumnBase<TGridItem> column, TGridItem item ) => column switch
	{
		CheckboxColumn<TGridItem> checkboxColumn => new CheckboxCell<TGridItem>( checkboxColumn, item ),
		ExpandColumn<TGridItem> expandColumn => new ExpandCell<TGridItem>( expandColumn, item ),
		_ => new DefaultCell<TGridItem>( column, item )
	};

	public CellBase<TGridItem> Create<TGridItem, TProp>( ColumnBase<TGridItem> column, TGridItem item ) => column switch
	{
		PasteableColumn<TGridItem, TProp> pasteColumn => new PasteableCell<TGridItem, TProp>( pasteColumn, item ),
		EditableColumn<TGridItem, TProp> editColumn => new EditableCell<TGridItem, TProp>( editColumn, item ),
		Column<TGridItem, TProp> propColumn => new PropertyCell<TGridItem, TProp>( propColumn, item ),
		_ => throw new NotImplementedException()
	};
}