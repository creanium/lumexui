// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

namespace LumexUI.Theme;

public record Palette
{
	/// <summary>
	/// Defines a primary color. The primary color is the color displayed most frequently across the app.
	/// </summary>
	public Color Primary { get; init; } = new Colors.Blue();

	/// <summary>
	/// Defines a secondary color. The secondary color provides more ways to accent and distinguish the app.
	/// </summary>
	public Color Secondary { get; init; } = new Colors.Violet();

	/// <summary>
	/// Defines a success color. The success color indicates positive or successful actions and information.
	/// </summary>
	public Color Success { get; init; } = new Colors.Green();

	/// <summary>
	/// Defines a warning color. The warning color indicates non-destructive warning messages.
	/// </summary> 
	public Color Warning { get; init; } = new Colors.Yellow();

	/// <summary>
	/// Defines a danger color. The danger color indicates errors and dangerous actions.
	/// </summary>
	public Color Danger { get; init; } = new Colors.Rose();

	/// <summary>
	/// Defines an info color. The info color indicates neutral and informative content.
	/// </summary>
	public Color Info { get; init; } = new Colors.LightBlue();

    /// <summary>
    /// Defines an info color. The info color indicates neutral and informative content.
    /// </summary>
    public Color Default { get; init; } = new Colors.Gray();

    /// <summary>
    /// Defines a background color. The background color is used for the background of the app and some components.
    /// </summary>
    public string Background { get; init; } = Colors.Contrast.White;

	/// <summary>
	/// Defines a foreground color. The foreground color is used for the text of the app and some components.
	/// </summary>
	public string Foreground { get; init; } = new Colors.Gray().S800;

	public Palette()
	{
		Default.Default = Default.S300;
	}
}
