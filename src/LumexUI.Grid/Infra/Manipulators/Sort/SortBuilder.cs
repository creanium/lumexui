// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using System.Linq.Expressions;

using LumexUI.Grid.Infra;

namespace LumexUI.Grid;

/// <summary>
/// Represents a sort order specification used within <see cref="LumexGrid{TGridItem}"/>.
/// </summary>
/// <typeparam name="TGridItem">The type of data represented by each row in the grid.</typeparam>
public class SortBuilder<TGridItem>
{
	internal SortDefinition<TGridItem> FirstSortDefinition { get; }

	internal SortBuilder( SortDefinition<TGridItem> firstSortDefinition )
	{
		FirstSortDefinition = firstSortDefinition;
	}

	/// <summary>
	/// Produces a <see cref="SortBuilder{T}"/> instance that sorts according to the specified <paramref name="expression"/>, ascending.
	/// </summary>
	/// <typeparam name="U">The type of the expression's value.</typeparam>
	/// <param name="expression">An expression defining how a set of <typeparamref name="TGridItem"/> instances are to be sorted.</param>
	/// <returns>A <see cref="SortBuilder{T}"/> instance representing the specified sorting rule.</returns>
	public static SortBuilder<TGridItem> ByAscending<TProp>( Expression<Func<TGridItem, TProp>> expression )
	{
		var sortDefinition = new SortDefinition<TGridItem>
		{
			Expression = ( queryable, asc ) => asc ? queryable.OrderBy( expression ) : queryable.OrderByDescending( expression )
		};

		return new SortBuilder<TGridItem>( sortDefinition );
	}

	/// <summary>
	/// Produces a <see cref="SortBuilder{T}"/> instance that sorts according to the specified <paramref name="expression"/>, descending.
	/// </summary>
	/// <typeparam name="U">The type of the expression's value.</typeparam>
	/// <param name="expression">An expression defining how a set of <typeparamref name="TGridItem"/> instances are to be sorted.</param>
	/// <returns>A <see cref="SortBuilder{T}"/> instance representing the specified sorting rule.</returns>
	public static SortBuilder<TGridItem> ByDescending<TProp>( Expression<Func<TGridItem, TProp>> expression )
	{
		var sortDefinition = new SortDefinition<TGridItem>
		{
			Expression = ( queryable, asc ) => asc ? queryable.OrderByDescending( expression ) : queryable.OrderBy( expression )
		};

		return new SortBuilder<TGridItem>( sortDefinition );
	}
}
