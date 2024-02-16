// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

namespace LumexUI.Theme;

public static class Breakpoints
{
	/// <summary>
	/// The media query breakpoint for small devices, such as landscape phones,
	/// with a viewport width of 480px and above.
	/// </summary>
	public const string SmallUp = "(min-width: 480px)";

	/// <summary>
	/// The media query breakpoint for medium devices, such as tablets,
	/// with a viewport width of 768px and above.
	/// </summary>
	public const string MediumUp = "(min-width: 768px)";

	/// <summary>
	/// The media query breakpoint for large devices, such as laptops,
	/// with a viewport width of 992px and above.
	/// </summary>
	public const string LargeUp = "(min-width: 992px)";

	/// <summary>
	/// The media query breakpoint for extra large devices, such as desktops,
	/// with a viewport width of 1200px and above.
	/// </summary>
	public const string XLargeUp = "(min-width: 1200px)";

	/// <summary>
	/// The media query breakpoint for extra small devices, such as portrait phones,
	/// with a viewport width less than 480px.
	/// </summary>
	public const string XSmallDown = "(max-width: 479.98px)";

	/// <summary>
	/// The media query breakpoint for extra small devices, such as landscape phones,
	/// with a viewport width less than 768px.
	/// </summary>
	public const string SmallDown = "(max-width: 767.98px)";

	/// <summary>
	/// The media query breakpoint for medium devices, such as tablets,
	/// with a viewport width less than 992px.
	/// </summary>
	public const string MediumDown = "(max-width: 991.98px)";

	/// <summary>
	/// The media query breakpoint for large devices, such as laptops,
	/// with a viewport width less than 1200px.
	/// </summary>
	public const string LargeDown = "(max-width: 1199.98px)";

	/// <summary>
	/// The media query breakpoint for only small devices, such as phones,
	/// with a viewport width in the range between 480px and 768px.
	/// </summary>
	public static string OnlySmall => Between( SmallUp, SmallDown );

	/// <summary>
	/// The media query breakpoint for only medium devices, such as tablets,
	/// with a viewport width in the range between 768px and 992px.
	/// </summary>
	public static string OnlyMedium => Between( MediumUp, MediumDown );

	/// <summary>
	/// The media query breakpoint for only large devices, such as laptops,
	/// with a viewport width in the range between 992px and 1200px.
	/// </summary>
	public static string OnlyLarge => Between( LargeUp, LargeDown );

	/// <summary>
	/// Combines two media queries using the `and` keyword. Requires values enclosed in parentheses.
	/// </summary>
	/// <param name="min">The minimum media query condition.</param>
	/// <param name="max">The maximum media query condition.</param>
	/// <returns>A combined media query using `and` with the specified minimum and maximum conditions.</returns>
	public static string Between( string min, string max )
	{
		return $"{min} and {max}";
	}
}
