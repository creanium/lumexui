// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using System.Linq.Expressions;

namespace LumexUI.Grid.Infra;

internal sealed class GridSortContext<TGridItem>
{
	private int _sortCount;
	private IDictionary<ColumnBase<TGridItem>, SortBuilder<TGridItem>> _sortableColumns = new Dictionary<ColumnBase<TGridItem>, SortBuilder<TGridItem>>();

	internal ColumnBase<TGridItem>? SortByColumn { get; private set; }
	internal bool SortByAscending { get; private set; }

	internal void SetSortByAscending( bool ascending ) => SortByAscending = ascending;
	internal void SetSortByColumn( ColumnBase<TGridItem> column )
	{
		_sortCount++;
		SortByColumn = _sortCount % 3 == 0 ? null : column;
	}

	internal void AddSortableColumn( ColumnBase<TGridItem> column, SortBuilder<TGridItem> sortBuilder )
	{
		if( !_sortableColumns.ContainsKey( column ) )
		{
			_sortableColumns.Add( column, sortBuilder );
		}
	}

	internal void AddSortableColumn<TProp>( ColumnBase<TGridItem> column, Expression<Func<TGridItem, TProp>> expression )
	{
		if( !_sortableColumns.ContainsKey( column ) )
		{
			var sortBuilder = SortBuilder<TGridItem>.ByAscending( expression );

			_sortableColumns.Add( column, sortBuilder );
		}
	}

	internal IQueryable<TGridItem> ApplySorting( IQueryable<TGridItem> source )
	{
		return SortByColumn is not null ? Apply( source ) : source;
	}

	private IOrderedQueryable<TGridItem> Apply( IQueryable<TGridItem> queryable )
	{
		var sortBuilder = _sortableColumns[SortByColumn!];

		return sortBuilder.FirstSortDefinition.Expression( queryable, SortByAscending );
	}
}