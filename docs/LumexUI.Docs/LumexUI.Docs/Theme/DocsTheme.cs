using LumexUI.Common;
using LumexUI.Theme;

namespace LumexUI.Docs.Theme;

internal record DocsTheme : LumexTheme
{
    internal DocsTheme()
    {
        DefaultTheme = ThemeType.Dark;

        // Typography
        var fontFamily = new FontFamily()
        {
            Sans = "Inter var",
            Mono = "Fira Code var"
        };

        // Dark theme
        Dark.Layout.FontFamily = fontFamily;

        Dark.Colors.Background = new ColorScale( Colors.Gray["900"] );
        Dark.Colors.Divider = new ColorScale( Colors.Gray["300"] );
        Dark.Colors.Primary = new ColorScale( Colors.Orange, "500" );

        // Light theme
        Light.Layout.FontFamily = fontFamily;
    }
}
