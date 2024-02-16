// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

namespace LumexUI.Grid.Data;

public class GridEditEventArgs<TGridItem> : EventArgs
{
	/// <summary>
	/// Specifies an item that has been affected.
	/// </summary>
	public TGridItem? Item { get; set; }

	/// <summary>
	/// Specfies an editing column id.
	/// </summary>
	public int ColumnId { get; set; }

	/// <summary>
	/// Specfies an editing column name.
	/// </summary>
	public string? ColumnName { get; set; }

	/// <summary>
	/// Specfies an editing column title.
	/// </summary>
	public string? ColumnTitle { get; set; }
}
