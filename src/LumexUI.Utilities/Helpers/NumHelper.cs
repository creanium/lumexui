// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

namespace LumexUI.Utilities;

public static class NumHelper
{
	public static double From<T>( T val )
	{
		return Convert.ToDouble( val );
	}

	public static T? To<T>( double val )
	{
		if( typeof( T ) == typeof( sbyte ) || typeof( T ) == typeof( sbyte? ) )
		{
			return (T)(object)Convert.ToSByte( val );
		}
		else if( typeof( T ) == typeof( byte ) || typeof( T ) == typeof( byte? ) )
		{
			return (T)(object)Convert.ToByte( val );
		}
		else if( typeof( T ) == typeof( short ) || typeof( T ) == typeof( short? ) )
		{
			return (T)(object)Convert.ToInt16( val );
		}
		else if( typeof( T ) == typeof( ushort ) || typeof( T ) == typeof( ushort? ) )
		{
			return (T)(object)Convert.ToUInt16( val );
		}
		else if( typeof( T ) == typeof( int ) || typeof( T ) == typeof( int? ) )
		{
			return (T)(object)Convert.ToInt32( val );
		}
		else if( typeof( T ) == typeof( uint ) || typeof( T ) == typeof( uint? ) )
		{
			return (T)(object)Convert.ToUInt32( val );
		}
		else if( typeof( T ) == typeof( long ) || typeof( T ) == typeof( long? ) )
		{
			return (T)(object)Convert.ToInt64( val );
		}
		else if( typeof( T ) == typeof( ulong ) || typeof( T ) == typeof( ulong? ) )
		{
			return (T)(object)Convert.ToUInt64( val );
		}
		else if( typeof( T ) == typeof( float ) || typeof( T ) == typeof( float? ) )
		{
			return (T)(object)Convert.ToSingle( val );
		}
		else if( typeof( T ) == typeof( decimal ) || typeof( T ) == typeof( decimal? ) )
		{
			return (T)(object)Convert.ToDecimal( val );
		}
		else if( typeof( T ) == typeof( double ) || typeof( T ) == typeof( double? ) )
		{
			return (T)(object)val;
		}

		return default;
	}
}
