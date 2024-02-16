// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using System.Reflection;

using LumexUI.Grid.Components;
using LumexUI.Grid.Data;

namespace LumexUI.Grid.Infra;

internal sealed class GridFilterContext<TGridItem>
{
	private readonly LumexGrid<TGridItem> _dataGrid;
	private readonly IEnumerable<IPropertyColumn?> _propertyColumns;
	private readonly IEnumerable<PropertyInfo?>? _stringProperties;

	internal GridFilterContext( LumexGrid<TGridItem> dataGrid )
	{
		_dataGrid = dataGrid;
		_propertyColumns = GetPropertyColumns();
		_stringProperties = GetPropertyColumnsProperties();
	}

	internal ValueTask<IQueryable<TGridItem>> FilterDataAsync( string filterString )
	{
		if( _dataGrid.Data is null )
		{
			return new ValueTask<IQueryable<TGridItem>>();
		}

		IQueryable<TGridItem> filteredItems;

		if( string.IsNullOrEmpty( filterString ) )
		{
			filteredItems = _dataGrid.Data;
		}
		else
		{
			filteredItems = GetFilteredItemsByQuery( filterString );
		}

		return ValueTask.FromResult( filteredItems );
	}

	private IQueryable<TGridItem> GetFilteredItemsByQuery( string filterString )
	{
		if( _stringProperties is null )
		{
			return default!;
		}

		var query = new Query();

		foreach( var property in _stringProperties )
		{
			if( property is null )
			{
				continue;
			}

			query.FilterCriteria.Add(
				new FilterCriteria
				{
					ModelProperty = property.Name,
					FilterString = filterString,
					FilterOperator = FilterOperator.Contains
				} );
		}

		return QueryExecutor<TGridItem>.Execute( query, _dataGrid.Data! );
	}

	private IEnumerable<IPropertyColumn?> GetPropertyColumns()
	{
		return _dataGrid.RenderedColumns.Select( c => c as IPropertyColumn );
	}

	private IEnumerable<PropertyInfo?> GetPropertyColumnsProperties()
	{
		return _propertyColumns.Where( c => c?.PropertyInfo?.PropertyType == typeof( string ) ).Select( c => c?.PropertyInfo );
	}
}