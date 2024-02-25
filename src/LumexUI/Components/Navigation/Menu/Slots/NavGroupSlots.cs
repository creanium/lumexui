// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using LumexUI.Common;

namespace LumexUI;

public record NavGroupSlots : ISlot
{
	public string? Root { get; init; }
	public string? Title { get; init; } 
	public string? Icon { get; init; }
	public string? Wrapper { get; init; }
	public string? Content { get; init; }
}
