// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using LumexUI.Grid.Data;
using LumexUI.Utilities;

namespace LumexUI.Grid;

/// <summary>
/// An abstract base class for cells in an <see cref="LumexGrid{TGridItem}"/>.
/// </summary>
/// <typeparam name="TGridItem">The type of data represented by each row in the grid.</typeparam>
public abstract class CellBase<TGridItem>
{
	/// <summary>
	/// Gets the data item represented by each row in the <see cref="LumexGrid{TGridItem}"/>.
	/// </summary>
	public TGridItem Item { get; }

	/// <summary>
	/// Gets the index of the column that the <see cref="CellBase{TGridItem}"/> is associated with.
	/// </summary>
	public int ColumnIndex { get; }

	/// <summary>
	/// Gets the CSS class name of the current instance of the <see cref="CellBase{TGridItem}"/>.
	/// Optionally overriden by derived classes.
	/// </summary>
	public virtual string Class =>
		new CssBuilder( "lumex-grid-cell" )
			.AddClass( Column.Class, when: !string.IsNullOrEmpty( Column.Class ) )
		.Build();

	/// <summary>
	/// Gets the instance of the <see cref="ColumnBase{TGridItem}"/> that the <see cref="CellBase{TGridItem}"/> is associated with.
	/// </summary>
	public ColumnBase<TGridItem> Column { get; }

	internal CellIdentifier<TGridItem> Identifier { get; }

	/// <summary>
	/// Constructs an instance of <see cref="CellBase{TGridItem}" />.
	/// </summary>
	/// <param name="column">The column this cell is associated with.</param>
	/// <param name="item">The data item represented by each row in the grid.</param>
	internal CellBase( ColumnBase<TGridItem> column, TGridItem item )
	{
		Item = item;
		Column = column;
		ColumnIndex = column.Index;
		Identifier = new CellIdentifier<TGridItem>( item, ColumnIndex );
	}

	/// <inheritdoc />
	public override bool Equals( object? obj )
	{
		return obj is CellBase<TGridItem> @base &&
				Identifier.Equals( @base.Identifier );
	}

	/// <inheritdoc />
	public override int GetHashCode()
	{
		return HashCode.Combine( Identifier );
	}

	/// <summary>
	/// Gets the custom CSS class name by invoking the <see cref="ColumnBase{TGridItem}.OnCellRender"/> callback
	/// from the given instance of the <see cref="ColumnBase{TGridItem}"/>.
	/// </summary>
	/// <param name="column">The column this cell is associated with.</param>
	/// <param name="value">The value this cell is associated with.</param>
	/// <returns>A <see cref="string"/> that represents the custom CSS class name for the cell.</returns>
	protected string? GetCustomCssClass( ColumnBase<TGridItem> column, object? value )
	{
		var args = new GridCellRenderArgs<TGridItem>( Item, value );

		column.OnCellRender?.Invoke( args );

		return args.Class;
	}
}
