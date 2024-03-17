// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

namespace LumexUI.Theme;

public record ThemeLayout
{
    public double DisabledOpacity { get; set; } = .6;
    public double HoverOpacity { get; set; } = .8;
    public string? BorderColor { get; set; }
}
