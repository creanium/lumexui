// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

namespace LumexUI.Theme;

public record LumexTheme
{
	/// <summary>
	/// Defines a color palette to be used for LumexUI components.
	/// </summary>
	public Palette Palette { get; init; } = new();

	/// <summary>
	/// Defines a typography to be used for LumexUI components.
	/// </summary>
	public Typography Typography { get; init; } = new();

	/// <summary>
	/// Defines a borders to be used for LumexUI components.
	/// </summary>
	public Borders Borders { get; init; } = new();
}
