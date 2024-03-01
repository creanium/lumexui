// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using LumexUI.Theme;

namespace LumexUI.Docs.Theme;

internal record Theme : LumexTheme
{
    internal Theme()
    {
        Palette = DocsDarkPalette;
        Typography = DocsTypography;
        Borders = DocsBorders;
    }

    private readonly Palette DocsDarkPalette = new()
    {
        Primary = new Colors.Orange(),
        Secondary = new Colors.Pink(),
        Background = new Colors.Gray().S900,
        Foreground = new Colors.Gray().S50
    };

    private readonly Typography DocsTypography = new()
    {
        FontFamilies = new()
        {
            SansSerif = "Inter var",
            Monospace = "Fira Code var"
        }
    };

    private readonly Borders DocsBorders = new()
    {
        Color = "#DBDBDB1a"
    };
}
