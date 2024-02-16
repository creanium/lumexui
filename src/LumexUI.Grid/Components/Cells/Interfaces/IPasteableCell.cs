// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

namespace LumexUI.Grid;

internal interface IPasteableCell : IEditableCell
{
	/// <summary>
	/// Defines a callback that is fired whenever a content is pasted into this cell.
	/// </summary>
	Func<ValueTask> OnPaste { get; }
}
