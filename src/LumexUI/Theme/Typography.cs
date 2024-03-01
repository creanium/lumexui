// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

namespace LumexUI.Theme;

public record Typography
{
    public FontFamilies FontFamilies { get; init; } = new();

	public FontSizes FontSizes { get; init; } = new();

	public LineHeights LineHeights { get; init; } = new();

	public string? FontWeight { get; init; } = "400";

	internal static string DefaultSansSerif => "system-ui,-apple-system,Segoe UI,Roboto,Helvetica Neue,Noto Sans,Liberation Sans,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji,Segoe UI Symbol,Noto Color Emoji;";

	internal static string DefaultMonospace => "SFMono-Regular,Menlo,Monaco,Consolas,Liberation Mono,Courier New,monospace";
}

public record FontFamilies
{
    public string? SansSerif { get; init; }
    public string? Monospace { get; init; }
}

public record FontSizes
{
	public string? Xs { get; init; } = ".75rem";
    public string? Sm { get; init; } = ".875rem";
    public string? Md { get; init; } = "1rem";
    public string? Lg { get; init; } = "1.25rem";
}

public record LineHeights
{
    public string? Xs { get; init; } = "1rem";
    public string? Sm { get; init; } = "1.25rem";
    public string? Md { get; init; } = "1.5rem";
    public string? Lg { get; init; } = "1.75rem";
}