// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

namespace LumexUI.Grid;

/// <summary>
/// A column that can perform pasting.
/// </summary>
internal interface IPasteableColumn
{
	void Paste( int startRowIndex, string[] values );
}
