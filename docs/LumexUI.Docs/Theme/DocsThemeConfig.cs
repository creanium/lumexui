// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using LumexUI.Common;
using LumexUI.Theme;

namespace LumexUI.Docs.Theme;

internal record DocsThemeConfig : LumexThemeConfig
{
    internal DocsThemeConfig()
    {
        DefaultTheme = ThemeType.Dark;
 
        // Dark theme
        DarkTheme.Layout.BorderColor = Colors.Gray["800"];
        DarkTheme.Colors.Background["default"] = Colors.Gray["900"];

        // Typography
        Typography.FontFamilies.SansSerif = "Inter var";
        Typography.FontFamilies.Monospace = "Fira Code var";
    }
}
