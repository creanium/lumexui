// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using LumexUI.Utilities;

namespace LumexUI.Theme;

/// <summary>
/// Represents an object for managing color shades.
/// </summary>
public record Color
{
	public string S50 { get; init; } = Colors.Contrast.White;
	public string S100 { get; init; } = Colors.Contrast.White;
	public string S200 { get; init; } = Colors.Contrast.White;
	public string S300 { get; init; } = Colors.Contrast.White;
	public string S400 { get; init; } = Colors.Contrast.White;
	public string S500 { get; init; } = Colors.Contrast.White;
	public string S600 { get; init; } = Colors.Contrast.White;
	public string S700 { get; init; } = Colors.Contrast.White;
	public string S800 { get; init; } = Colors.Contrast.White;
	public string S900 { get; init; } = Colors.Contrast.White;
	public string S950 { get; init; } = Colors.Contrast.White;
	public string Default => S400;
	public string Contrast => ColorUtils.Contrast( Default );

	internal (string Shade, string Value)[] GetShades()
	{
		(string, string)[] values = new[]
		{
			( "50", S50 ),
			( "100", S100 ),
			( "200", S200 ),
			( "300", S300 ),
			( "400", S400 ),
			( "500", S500 ),
			( "600", S600 ),
			( "700", S700 ),
			( "800", S800 ),
			( "900", S900 ),
			( "950", S950 ),
			( "", Default ),
			( "contrast", Contrast )
		};

		return values;
	}
}
