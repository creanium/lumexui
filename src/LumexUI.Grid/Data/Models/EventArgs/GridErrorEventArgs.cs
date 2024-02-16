// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

namespace LumexUI.Grid.Data;

public class GridErrorEventArgs : EventArgs
{
	public Exception Exception { get; set; }

	public string? Message { get; set; }

	public GridErrorEventArgs( Exception ex, string? message )
	{
		Exception = ex;
		Message = message;
	}
}
