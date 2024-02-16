// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using System.Numerics;

namespace LumexUI.Utilities;

public static class TypeHelper
{
	private static readonly HashSet<Type> NumericTypes = new()
	{
			typeof(sbyte),
			typeof(byte),
			typeof(short),
			typeof(ushort),
			typeof(int),
			typeof(uint),
			typeof(long),
			typeof(ulong),
			typeof(float),
			typeof(double),
			typeof(decimal),
			typeof(BigInteger)
	};

	public static bool IsNumeric( Type type )
	{
		return NumericTypes.Contains( type ) ||
			   NumericTypes.Contains( Nullable.GetUnderlyingType( type )! );
	}

	public static bool IsString( Type type )
	{
		return typeof( string ) == type;
	}

	public static bool IsDateTime( Type type )
	{
		return typeof( DateTime ) == type || typeof( DateTime? ) == type;
	}

	public static U? ConvertFromTo<T, U>( T value )
	{
		bool stringValueAndEmpty = value is string strVal && string.IsNullOrWhiteSpace( strVal );

		if( value is null || stringValueAndEmpty )
		{
			return default;
		}

		try
		{
			Type type = typeof( U );

			if( type.IsGenericType && type.GetGenericTypeDefinition().Equals( typeof( Nullable<> ) ) )
			{
				type = Nullable.GetUnderlyingType( type ) ?? type;
			}

			return (U?)Convert.ChangeType( value, type );
		}
		catch( Exception ex )
		{
			throw new Exception( ex.Message, ex );
		}
	}
}
