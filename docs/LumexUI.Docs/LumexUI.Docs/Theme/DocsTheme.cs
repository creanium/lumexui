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
        Dark.Colors.Divider["default"] = Colors.Gray["800"];
        Dark.Colors.Background["default"] = Colors.Gray["900"];
        Dark.Layout.FontFamily = fontFamily;

        // Light theme
        Light.Layout.FontFamily = fontFamily;
    }
}
