// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

namespace LumexUI.Theme;

public record Typography
{
	public string? SansSerif { get; init; }

	public string? Monospace { get; init; }

	public string FontSize { get; init; } = "1rem";

	public string FontWeight { get; init; } = "400";

	public string LineHeight { get; init; } = "1.5";

	internal string DefaultSansSerif => "system-ui,-apple-system,Segoe UI,Roboto,Helvetica Neue,Noto Sans,Liberation Sans,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji,Segoe UI Symbol,Noto Color Emoji;";

	internal string DefaultMonospace => "SFMono-Regular,Menlo,Monaco,Consolas,Liberation Mono,Courier New,monospace";
}
