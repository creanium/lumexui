// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using LumexUI.Common;

namespace LumexUI.Theme;

public record DarkThemeConfig : ThemeConfig
{
    public DarkThemeConfig()
    {
        Type = ThemeType.Dark;
        Layout = new()
        {
            BorderColor = Theme.Colors.Gray["700"],
            HoverOpacity = .9
        };
        Colors = SemanticColors.Dark;
    }
}
