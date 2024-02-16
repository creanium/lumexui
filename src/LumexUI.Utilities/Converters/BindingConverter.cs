// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using System.Globalization;

namespace LumexUI.Utilities;

public static class BindingConverter
{
	public static bool TryConvertTo<T>( string? value, CultureInfo? culture, out T? result )
	{
		if( string.IsNullOrEmpty( value ) )
		{
			result = default;
			return false;
		}

		if( typeof( T ) == typeof( string ) )
		{
			result = (T)(object)value;
			return true;
		}
		else if( typeof( T ) == typeof( bool ) || typeof( T ) == typeof( bool? ) )
		{
			if( int.TryParse( value, out var converted ) )
			{
				result = (T)(object)converted;
				return true;
			}
		}
		else if( typeof( T ) == typeof( int ) || typeof( T ) == typeof( int? ) )
		{
			if( int.TryParse( value, NumberStyles.Number, culture ?? CultureInfo.CurrentCulture, out var converted ) )
			{
				result = (T)(object)converted;
				return true;
			}
		}
		else if( typeof( T ) == typeof( short ) || typeof( T ) == typeof( short? ) )
		{
			if( short.TryParse( value, NumberStyles.Number, culture ?? CultureInfo.CurrentCulture, out var converted ) )
			{
				result = (T)(object)converted;
				return true;
			}
		}
		else if( typeof( T ) == typeof( long ) || typeof( T ) == typeof( long? ) )
		{
			if( long.TryParse( value, NumberStyles.Number, culture ?? CultureInfo.CurrentCulture, out var converted ) )
			{
				result = (T)(object)converted;
				return true;
			}
		}
		else if( typeof( T ) == typeof( float ) || typeof( T ) == typeof( float? ) )
		{
			if( float.TryParse( value, NumberStyles.Number, culture ?? CultureInfo.CurrentCulture, out var converted ) )
			{
				result = (T)(object)converted;
				return true;
			}
		}
		else if( typeof( T ) == typeof( double ) || typeof( T ) == typeof( double? ) )
		{
			if( double.TryParse( value, NumberStyles.Number, culture ?? CultureInfo.CurrentCulture, out var converted ) )
			{
				result = (T)(object)converted;
				return true;
			}
		}
		else if( typeof( T ) == typeof( decimal ) || typeof( T ) == typeof( decimal? ) )
		{
			if( decimal.TryParse( value, NumberStyles.Number, culture ?? CultureInfo.CurrentCulture, out var converted ) )
			{
				result = (T)(object)converted;
				return true;
			}
		}

		result = default;
		return false;
	}
}
