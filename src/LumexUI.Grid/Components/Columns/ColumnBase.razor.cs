// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using LumexUI.Grid.Components;
using LumexUI.Grid.Data;
using LumexUI.Grid.Infra;
using LumexUI.Grid.Services;
using LumexUI.Utilities;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web.Virtualization;

namespace LumexUI.Grid;

/// <summary>
/// An abstract base class for columns in an <see cref="LumexGrid{TGridItem}"/>.
/// </summary>
/// <typeparam name="TGridItem">The type of data represented by each row in the grid.</typeparam>
public abstract partial class ColumnBase<TGridItem>
{
	[CascadingParameter] internal InternalGridContext<TGridItem> InternalGridContext { get; set; } = default!;

	/// <summary>
	/// Defines an optional template for this column's header cell. 
	/// If not specified, the default header template includes the <see cref="Title" />.
	/// </summary>
	[Parameter] public RenderFragment<ColumnBase<TGridItem>>? HeaderTemplate { get; set; }

	/// <summary>
	/// Defines an optional placeholder template for this column's body cells.
	/// If specified, virtualized grids will use this template to render cells whose data has not yet been loaded.
	/// </summary>
	[Parameter] public RenderFragment<PlaceholderContext>? PlaceholderTemplate { get; set; }

	/// <summary>
	/// Specifies an optional title text for this column. 
	/// This is rendered automatically if <see cref="HeaderTemplate" /> is not used.
	/// </summary>
	[Parameter] public string? Title { get; set; }

	/// <summary>
	/// Specifies an optional width of this column.
	/// </summary>
	[Parameter] public string? Width { get; set; }

	/// <summary>
	/// Specifies an optional CSS class name for this column cells (excluding the header). 
	/// If provided, this will be included in the class attribute of cells (excluding the header) for this column.
	/// </summary>
	[Parameter] public string? Class { get; set; }

	/// <summary>
	/// Specifies an optional CSS class name for this column header cell. 
	/// If provided, this will be included in the class attribute of the header cell for this column.
	/// </summary>
	[Parameter] public string? HeaderClass { get; set; }

	/// <summary>
	/// Indicates whether this column is visible.
	/// </summary>
	/// <remarks>Default is <see langword="true"/></remarks>
	[Parameter] public bool Visible { get; set; } = true;

	/// <summary>
	/// Defines a callback that is fired whenever a column cell is rendered. For example, you may define
	/// a custom CSS class or classes for the cell based on the data.
	/// </summary>
	[Parameter] public Action<GridCellRenderArgs<TGridItem>>? OnCellRender { get; set; }

	[Inject] protected ICellFactory CellFactory { get; set; } = default!;

	/// <summary>
	/// Gets or sets a <see cref="RenderFragment" /> that will be rendered for this column's header cell.
	/// This allows derived components to change the header output. However, derived components are then
	/// responsible for using <see cref="HeaderTemplate" /> within that new output if they want to continue
	/// respecting that option.
	/// </summary>
	protected internal RenderFragment HeaderCell { get; protected set; }

	/// <summary>
	/// Gets a reference to the enclosing <see cref="LumexGrid{TGridItem}" />.
	/// </summary>
	internal LumexGrid<TGridItem> Grid => InternalGridContext.Grid;

	/// <summary>
	/// Defines an index for this column.
	/// </summary>
	internal int Index { get; private set; }

	private string? ColumnHeaderCssClass =>
		new CssBuilder( "lumex-grid-cell" )
			.AddClass( Class, when: !string.IsNullOrEmpty( Class ) )
			.AddClass( HeaderClass, when: !string.IsNullOrEmpty( HeaderClass ) )
		.Build();

	private string? ColumnPlaceholderCssClass =>
		new CssBuilder( "lumex-grid-cell lumex-grid-cell-placeholder" )
			.AddClass( Class, when: !string.IsNullOrEmpty( Class ) )
		.Build();

	/// <summary>
	/// Constructs an instance of <see cref="ColumnBase{TGridItem}" />.
	/// </summary>
	public ColumnBase()
	{
		HeaderCell = RenderHeaderCell;
	}

	/// <summary>
	/// Overridden by derived components to provide cell creation logic for the column.
	/// </summary>
	/// <param name="item">The data for the row being rendered.</param>
	/// <returns>An appropriate cell object for this column.</returns>
	protected abstract CellBase<TGridItem> CreateCell( TGridItem item );

	private void SetColumnId()
	{
		Index = Grid.RenderedColumns.IndexOf( this );
	}

	private string? AriaSortValue( ColumnBase<TGridItem> column )
	{
		if( column is ISortableColumn<TGridItem> sortableColumn && sortableColumn.Sortable.HasValue )
		{
			if( Grid.GridSortContext.SortByColumn != sortableColumn )
			{
				return "none";
			}

			return Grid.GridSortContext.SortByAscending ? "ascending" : "descending";
		}

		return null;
	}
}