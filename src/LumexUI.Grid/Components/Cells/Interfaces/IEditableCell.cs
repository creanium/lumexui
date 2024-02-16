// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using Microsoft.AspNetCore.Components.Web;

namespace LumexUI.Grid;

internal interface IEditableCell : IPropertyCell
{
	/// <summary>
	/// Specifies the callback that is fired whenever the cell is clicked.
	/// </summary>
	Func<ValueTask> OnClick { get; set; }

	/// <summary>
	/// Specifies the callback that is fired whenever the key is released on the cell element.
	/// </summary>
	Func<KeyboardEventArgs, ValueTask> OnKeyUp { get; set; }
}