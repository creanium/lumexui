// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

namespace LumexUI.Theme;

public record Borders
{
	public string Color { get; init; } = new Colors.Gray().S100; // TODO: Change?

	public string Sm { get; init; } = ".25rem";

	public string Md { get; init; } = ".375rem";

	public string Lg { get; init; } = ".5rem";

	public string Xl { get; init; } = ".75rem";

	public string Xxl { get; init; } = "1rem";
}