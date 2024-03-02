// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

namespace LumexUI.Theme;

public record Palette
{
    /// <summary>
    /// Defines a primary color. The primary color is the color displayed most frequently across the app.
    /// </summary>
    public ColorScale Primary { get; init; } = Colors.Blue;

    /// <summary>
    /// Defines a secondary color. The secondary color provides more ways to accent and distinguish the app.
    /// </summary>
    public ColorScale Secondary { get; init; } = Colors.Violet;

    /// <summary>
    /// Defines a success color. The success color indicates positive or successful actions and information.
    /// </summary>
    public ColorScale Success { get; init; } = Colors.Green;

    /// <summary>
    /// Defines a warning color. The warning color indicates non-destructive warning messages.
    /// </summary> 
    public ColorScale Warning { get; init; } = Colors.Yellow;

    /// <summary>
    /// Defines a danger color. The danger color indicates errors and dangerous actions.
    /// </summary>
    public ColorScale Danger { get; init; } = Colors.Rose;

    /// <summary>
    /// Defines an info color. The info color indicates neutral and informative content.
    /// </summary>
    public ColorScale Info { get; init; } = Colors.LightBlue;

    /// <summary>
    /// Defines an info color. The info color indicates neutral and informative content.
    /// </summary>
    public ColorScale Default { get; init; } = Colors.Gray;

    /// <summary>
    /// Defines a background color. The background color is used for the background of the app and some components.
    /// </summary>
    public string Background { get; init; } = Colors.Contrast.White;

    /// <summary>
    /// Defines a foreground color. The foreground color is used for the text of the app and some components.
    /// </summary>
    public string Foreground { get; init; } = Colors.Gray.S800;

    /// <summary>
    /// Defines a focus color. The focus color is used for the elements that has received focus.
    /// </summary>
    public string Focus { get; init; } = Colors.Blue.S500;

    public Palette()
    {
        Default.SetDefault( 300 );
    }
}
