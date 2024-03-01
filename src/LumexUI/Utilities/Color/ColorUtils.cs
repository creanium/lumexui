// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using System.Globalization;

namespace LumexUI.Utilities;

internal static class ColorUtils
{
	private const string LightColor = Colors.Contrast.White;
    private const string DarkColor = Colors.Contrast.Black;

	internal static string FromHexToRgbCss( string color, double alpha = 1 )
	{
		FromHexToRgb( color, out byte R, out byte G, out byte B );

		return $"rgb({R}, {G}, {B}, {alpha})";
	}

	internal static string FromHexToRgb( string color )
	{
		FromHexToRgb( color, out byte R, out byte G, out byte B );

		return $"{R}, {G}, {B}";
	}

	internal static string Contrast( string color )
	{
		return Luminance( color ) < .5 ? LightColor : DarkColor;
	}

	internal static string Tint( string color, double weight )
	{
		return Mix( LightColor, color, weight );
	}

	internal static string Shade( string color, double weight )
	{
		return Mix( DarkColor, color, weight );
	}

	private static string Mix( string color1, string color2, double weight )
	{
		var result = "#";

		FromHexToRgb( color1, out byte R1, out byte G1, out byte B1 );
		FromHexToRgb( color2, out byte R2, out byte G2, out byte B2 );

		byte[] RGBs = [R1, R2, G1, G2, B1, B2];

		for( int i = 0; i < RGBs.Length; i += 2 )
		{
			var hex = ( (byte)Math.Round( RGBs[i] * weight + RGBs[i + 1] * ( 1 - weight ) ) ).ToString( "X" );

			while( hex.Length < 2 )
			{
				hex = '0' + hex;
			}

			result += hex;
		}

		return result;
	}

	private static void FromHexToRgb( string color, out byte R, out byte G, out byte B )
	{
		color = color[1..];

		if( color == null || !uint.TryParse( color, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var decimalValue ) )
		{
			throw new ArgumentException( $"Color hexadecimal value `{color}` is not in the correct format.", nameof( color ) );
		}

		R = (byte)( decimalValue >> 16 );
		G = (byte)( decimalValue >> 8 );
		B = (byte)( decimalValue >> 0 );
	}

	private static void FromHexToHsl( string color, out double H, out double S, out double L )
	{
		FromHexToRgb( color, out byte R, out byte G, out byte B );

		var _R = R / 255d;
		var _G = G / 255d;
		var _B = B / 255d;

		var max = Math.Max( _R, Math.Max( _G, _B ) );
		var min = Math.Min( _R, Math.Min( _G, _B ) );
		var delta = max - min;

		H = 0;
		S = 0;
		L = ( max + min ) / 2d;

		if( delta != 0 )
		{
			S = L < 0.5
				? delta / ( max + min )
				: delta / ( 2d - max - min );

			if( _R == max )
			{
				H = ( _G - _B ) / delta;
			}
			else if( _G == max )
			{
				H = 2d + ( ( _B - _R ) / delta );
			}
			else if( _B == max )
			{
				H = 4d + ( ( _R - _G ) / delta );
			}
		}

		H = Math.Round( H *= 60d, 2 );
		S = Math.Round( S *= 100, 2 );
		L = Math.Round( L *= 100, 2 );

		if( H < 0 )
		{
			H += 360;
		}
	}

	private static double Luminance( string color )
	{
		FromHexToRgb( color, out byte R, out byte G, out byte B );

		double[] RGB = { R, G, B };

		for( int i = 0; i < RGB.Length; i++ )
		{
			RGB[i] /= 255.0;

			RGB[i] = RGB[i] <= 0.03928
				? RGB[i] / 12.92
				: Math.Pow( ( RGB[i] + 0.055 ) / 1.055, 2.4 );
		}

		return ( RGB[0] * 0.2126 ) + ( RGB[1] * 0.7152 ) + ( RGB[2] * 0.0722 );
	}
}
