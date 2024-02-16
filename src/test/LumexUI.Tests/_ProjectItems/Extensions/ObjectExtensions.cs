// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using System.Reflection;

namespace LumexUI.Tests.ProjectItems.Extensions;

internal static class ObjectExtensions
{
	public static object? GetPropertyValue( this object self, string property )
	{
		var pi = GetPrivateProperty( self, property );

		return pi?.GetValue( self );
	}

	public static object? GetFieldValue( this object self, string field )
	{
		var fi = GetPrivateField( self, field );

		return fi?.GetValue( self );
	}

	private static PropertyInfo? GetPrivateProperty( object self, string property )
	{
		return self.GetType().GetProperty( property, BindingFlags.NonPublic | BindingFlags.Instance );
	}

	private static FieldInfo? GetPrivateField( object self, string field )
	{
		return self.GetType().GetField( field, BindingFlags.NonPublic | BindingFlags.Instance );
	}
}
