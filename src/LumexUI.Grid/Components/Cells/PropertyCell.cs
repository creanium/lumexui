// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using LumexUI.Grid.Data;
using LumexUI.Utilities;

namespace LumexUI.Grid;

internal class PropertyCell<TGridItem, TProp> : CellBase<TGridItem>, IPropertyCell
{
	/// <summary>
	/// Defines the display value of the <see cref="PropertyCell{TGridItem, TProp}"/>.
	/// This will be included in the HTML <c>title</c> attribute of the cell element.
	/// </summary>
	public string? DisplayValue { get; }

	/// <inheritdoc />
	public override string Class =>
		new CssBuilder( base.Class )
			.AddClass( GetCustomCssClass( GetPropertyColumn(), Value.RawValue ) )
		.Build();

	/// <summary>
	/// Defines the <see cref="CellValue"/> associated with the <see cref="PropertyCell{TGridItem, TProp}"/>.
	/// This holds both raw and formatted values.
	/// </summary>
	protected CellValue Value { get; }

	/// <summary>
	/// Constructs an instance of <see cref="PropertyCell{TGridItem, TProp}" />.
	/// </summary>
	/// <param name="column">The column this cell is associated with.</param>
	/// <param name="item">The data item represented by each row in the grid.</param>
	public PropertyCell( Column<TGridItem, TProp> column, TGridItem item ) : base( column, item )
	{
		Value = column.GetCellValue( item );
		DisplayValue = Value.FormattedValue ?? (string?)Value.RawValue;
	}

	private Column<TGridItem, TProp> GetPropertyColumn() => (Column<TGridItem, TProp>)Column;
}
