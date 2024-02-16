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
        Tertiary = new Colors.Teal(),
        Background = new Colors.Gray().S950,
        Foreground = new Colors.Neutral().S100
    };

    private readonly Typography DocsTypography = new()
    {
        SansSerif = "Inter var",
        Monospace = "Fira Code var"
    };

    private readonly Borders DocsBorders = new()
    {
        Color = "#DBDBDB1a"
    };
}
