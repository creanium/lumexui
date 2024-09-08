using LumexUI.Common;
using LumexUI.Theme;

namespace LumexUI.Docs.Theme;

internal record DocsTheme : LumexTheme
{
    internal DocsTheme()
    {
        DefaultTheme = ThemeType.Light;

        // Typography
        var fontFamily = new FontFamily()
        {
            Sans = "Inter var",
            Mono = "Fira Code var"
        };

        // Dark theme
        Dark.Layout.FontFamily = fontFamily;
        Dark.Colors.Background = new ColorScale( Colors.Zinc["950"] );

        // Light theme
        Light.Layout.FontFamily = fontFamily;
    }
}
