// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using LumexUI.Utilities;

namespace LumexUI.Grid;

internal class DefaultCell<TGridItem> : CellBase<TGridItem>
{
	/// <inheritdoc />
	public override string Class =>
		new CssBuilder( base.Class )
			.AddClass( GetCustomCssClass( Column, null ) )
		.Build();

	/// <summary>
	/// Constructs an instance of <see cref="DefaultCell{TGridItem}{TGridItem}" />.
	/// </summary>
	/// <param name="column">The column this cell is associated with.</param>
	/// <param name="item">The data item represented by each row in the grid.</param>
	public DefaultCell( ColumnBase<TGridItem> column, TGridItem item ) : base( column, item )
	{
	}
}
