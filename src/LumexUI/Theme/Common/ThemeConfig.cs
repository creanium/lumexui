// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using LumexUI.Common;

namespace LumexUI.Theme;

public record ThemeConfig
{
	public ThemeLayout Layout { get; set; } = new();
	public ThemeColors Colors { get; set; } = new();
	public ThemeType Type { get; internal set; }
}
