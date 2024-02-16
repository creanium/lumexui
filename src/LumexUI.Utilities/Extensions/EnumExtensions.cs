// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using System.ComponentModel;

namespace LumexUI.Utilities;

public static class EnumExtensions
{
	public static string ToDescription( this Enum value )
	{
		var attributes = (DescriptionAttribute[])value.GetType().GetField( value.ToString() )!.GetCustomAttributes( typeof( DescriptionAttribute ), inherit: false );

		if( attributes == null || attributes.Length == 0 )
		{
			return value.ToLowerInvariant();
		}

		return attributes[0].Description;
	}

	public static string ToLowerInvariant( this Enum value )
	{
		return value.ToString().ToLowerInvariant();
	}
}
