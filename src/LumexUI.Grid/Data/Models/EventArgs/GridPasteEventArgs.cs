// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

namespace LumexUI.Grid.Data;

public class GridPasteEventArgs<TGridItem> : EventArgs
{
	/// <summary>
	/// Specifies an item that has been affected.
	/// </summary>
	public TGridItem Item { get; set; }

	/// <summary>
	/// Specifies a value to be pasted.
	/// </summary>
	public string Value { get; set; }

	/// <summary>
	/// Indicates whether the value is valid.
	/// </summary>
	public bool Valid { get; set; } = true;

	public GridPasteEventArgs( TGridItem item, string value )
	{
		Item = item;
		Value = value;
	}
}
