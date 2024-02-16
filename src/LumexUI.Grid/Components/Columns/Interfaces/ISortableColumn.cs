// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using LumexUI.Grid.Data;

namespace LumexUI.Grid.Components;

/// <summary>
/// A column that can perform sorting.
/// </summary>
public interface ISortableColumn<TGridItem>
{
	/// <summary>
	/// Indicates whether the data should be sortable by this column. 
	/// </summary>
	bool? Sortable { get; set; }

	/// <summary>
	/// Specifies that this column represents the initial sort order
	/// for the grid. The supplied value controls the default sort direction.
	/// </summary>
	SortDirection? DefaultSort { get; set; }

	/// <summary>
	/// Optionally specifies sorting rules for this column instance.
	/// </summary>
	SortBuilder<TGridItem>? SortBy { get; set; }
}
