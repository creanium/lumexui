// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using LumexUI.Grid.Components;
using LumexUI.Grid.Data;

using Microsoft.AspNetCore.Components;

namespace LumexUI.Grid;

/// <summary>
/// Represents a <see cref="LumexGrid{TGridItem}"/> column whose cells render a supplied template.
/// </summary>
/// <typeparam name="TGridItem">The type of data represented by each row in the grid.</typeparam>
public partial class TemplateColumn<TGridItem> : ColumnBase<TGridItem>, ISortableColumn<TGridItem>
{
	/// <summary>
	/// Defines an optional content template for this column's body cell. 
	/// </summary>
	[Parameter] public RenderFragment<TGridItem>? Template { get; set; }

	/// <summary>
	/// Indicates whether the data should be sortable by this column. 
	/// </summary>
	[Parameter] public bool? Sortable { get; set; }

	/// <summary>
	/// Specifies that this column represents the initial sort order
	/// for the grid. The supplied value controls the default sort direction.
	/// </summary>
	[Parameter] public SortDirection? DefaultSort { get; set; }

	/// <summary>
	/// Optionally specifies sorting rules for this column instance.
	/// </summary>
	[Parameter] public SortBuilder<TGridItem>? SortBy { get; set; }

	protected override void OnParametersSet()
	{
		TryAddSortableColumn();
	}

	protected override CellBase<TGridItem> CreateCell( TGridItem item )
	{
		return CellFactory.Create( this, item );
	}

	/// <summary>
	/// Indicates whether this column should act as sortable if no value was set for the <see cref="ISortableColumn{TGridItem}.Sortable" /> parameter. 
	/// 
	/// <para>The default behavior is not to be sortable unless <see cref="ISortableColumn{TGridItem}.Sortable" /> is <see langword="true"/></para>
	///
	/// Derived components may override this to implement alternative default sortability rules.
	/// </summary>
	/// <returns><see langword="true"/> if the column should be sortable by default, otherwise <see langword="false"/>.</returns>
	protected virtual bool IsSortableByDefault() => SortBy is not null;

	protected virtual void TryAddSortableColumn()
	{
		if( SortBy is not null )
		{
			Grid.GridSortContext.AddSortableColumn( this, SortBy );

			return;
		}
	}
}
