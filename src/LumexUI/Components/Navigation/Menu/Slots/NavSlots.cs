// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using LumexUI.Common;

namespace LumexUI;

public record NavSlots : ISlot
{
	public string? Root { get; init; }
	public string? Group { get; init; }
	public string? Item { get; init; }
}
