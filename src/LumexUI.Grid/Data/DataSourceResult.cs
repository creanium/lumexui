// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

namespace LumexUI.Grid.Data;

/// <summary>
/// Holds data being supplied to a <see cref="LumexGrid{TGridItem}"/>'s <see cref="LumexGrid{TGridItem}.DataSource"/>.
/// </summary>
/// <typeparam name="TGridItem">The type of data represented by each row in the grid.</typeparam>
public readonly struct DataSourceResult<TGridItem>
{
	/// <summary>
	/// The items being supplied.
	/// </summary>
	public ICollection<TGridItem> Data { get; init; }

	/// <summary>
	/// The total number of items that may be displayed in the grid. This normally means the total number of items in the
	/// underlying data source after applying any filtering that is in effect.
	///
	/// If the grid is paginated, this should include all pages. If the grid is virtualized, this should include the entire scroll range.
	/// </summary>
	public int TotalItemCount { get; init; }
}

/// <summary>
/// Provides convenience methods for constructing <see cref="DataSourceResult{TGridItem}"/> instances.
/// </summary>
public static class DataSourceResult
{
	/// <summary>
	/// Supplies an instance of <see cref="DataSourceResult{TGridItem}"/>.
	/// </summary>
	/// <typeparam name="TGridItem">The type of data represented by each row in the grid.</typeparam>
	/// <param name="data">The items being supplied.</param>
	/// <param name="totalItemCount">The total numer of items that exist. See <see cref="DataSourceResult{TGridItem}.TotalItemCount"/> for details.</param>
	/// <returns>An instance of <see cref="DataSourceResult{TGridItem}"/>.</returns>
	public static DataSourceResult<TGridItem> From<TGridItem>( ICollection<TGridItem> data, int totalItemCount )
		=> new() { Data = data, TotalItemCount = totalItemCount };
}