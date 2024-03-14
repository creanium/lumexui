// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using LumexUI.Common;

namespace LumexUI.Theme;

public record LumexThemeConfig
{
    public Borders Borders { get; set; } = new();
    public Typography Typography { get; set; } = new();
    public LightThemeConfig LightTheme { get; set; } = new();
    public DarkThemeConfig DarkTheme { get; set; } = new();
    public ThemeType DefaultTheme { get; set; }
}
