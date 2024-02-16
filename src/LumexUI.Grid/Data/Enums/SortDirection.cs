// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

namespace LumexUI.Grid.Data;

/// <summary>
/// Describes the direction in which a <see cref="LumexGrid{TGridItem}"/> column is sorted.
/// </summary>
public enum SortDirection
{
	/// <summary>
	/// Ascending sort order.
	/// </summary>
	Ascending,

	/// <summary>
	/// Descending sort order.
	/// </summary>
	Descending,

	/// <summary>
	/// Automatic sort order. When used with <see cref="LumexGrid{TGridItem}.SortByColumnAsync(ColumnBase{TGridItem}, SortDirection)"/>,
	/// the sort order will automatically toggle between <see cref="Ascending"/> and <see cref="Descending"/> on successive calls, and
	/// resets to <see cref="Ascending"/> whenever the specified column is changed.
	/// </summary>
	Auto
}
