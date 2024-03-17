// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

namespace LumexUI.Theme;

public record Typography
{
	public int FontWeight { get; set; }
	public FontScale FontSizes { get; set ; }
	public FontScale LineHeights { get; set; }
    public FontFamily FontFamilies { get; set; }

    public Typography()
    {
        FontWeight = 400;
        FontSizes = new()
        {
            Xs = ".75rem",
            Sm = ".875rem",
            Md = "1rem",
            Lg = "1.25rem"
        };
        LineHeights = new()
        {
            Xs = "1rem",
            Sm = "1.25rem",
            Md = "1.5rem",
            Lg = "1.75rem"
        };
        FontFamilies = new();
    }
}

public record FontFamily
{
    internal static string DefaultSansSerif => "system-ui,-apple-system,Segoe UI,Roboto,Helvetica Neue,Noto Sans,Liberation Sans,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji,Segoe UI Symbol,Noto Color Emoji;";
    internal static string DefaultMonospace => "SFMono-Regular,Menlo,Monaco,Consolas,Liberation Mono,Courier New,monospace";

    public string SansSerif { get; set; } = DefaultSansSerif;
    public string Monospace { get; set; } = DefaultMonospace;
}

public record FontScale : BaseScale
{
    public string? Xs { get; set; }
}