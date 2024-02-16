// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using System.Numerics;

using LumexUI.Utilities;

namespace LumexUI.Tests.Utilities;

public class TypeHelperTest
{
	[Fact]
	public void IsNumeric_ForNumericTypes_ReturnsTrue()
	{
		TypeHelper.IsNumeric( typeof( sbyte ) ).Should().BeTrue();
		TypeHelper.IsNumeric( typeof( sbyte? ) ).Should().BeTrue();
		TypeHelper.IsNumeric( typeof( byte ) ).Should().BeTrue();
		TypeHelper.IsNumeric( typeof( byte? ) ).Should().BeTrue();
		TypeHelper.IsNumeric( typeof( short ) ).Should().BeTrue();
		TypeHelper.IsNumeric( typeof( short? ) ).Should().BeTrue();
		TypeHelper.IsNumeric( typeof( ushort ) ).Should().BeTrue();
		TypeHelper.IsNumeric( typeof( ushort? ) ).Should().BeTrue();
		TypeHelper.IsNumeric( typeof( int ) ).Should().BeTrue();
		TypeHelper.IsNumeric( typeof( int? ) ).Should().BeTrue();
		TypeHelper.IsNumeric( typeof( uint ) ).Should().BeTrue();
		TypeHelper.IsNumeric( typeof( uint? ) ).Should().BeTrue();
		TypeHelper.IsNumeric( typeof( long ) ).Should().BeTrue();
		TypeHelper.IsNumeric( typeof( long? ) ).Should().BeTrue();
		TypeHelper.IsNumeric( typeof( ulong ) ).Should().BeTrue();
		TypeHelper.IsNumeric( typeof( ulong? ) ).Should().BeTrue();
		TypeHelper.IsNumeric( typeof( float ) ).Should().BeTrue();
		TypeHelper.IsNumeric( typeof( float? ) ).Should().BeTrue();
		TypeHelper.IsNumeric( typeof( double ) ).Should().BeTrue();
		TypeHelper.IsNumeric( typeof( double? ) ).Should().BeTrue();
		TypeHelper.IsNumeric( typeof( decimal ) ).Should().BeTrue();
		TypeHelper.IsNumeric( typeof( decimal? ) ).Should().BeTrue();
		TypeHelper.IsNumeric( typeof( BigInteger ) ).Should().BeTrue();
		TypeHelper.IsNumeric( typeof( BigInteger? ) ).Should().BeTrue();
	}

	[Fact]
	public void IsNumeric_ForNonNumericType_ReturnsFalse()
	{
		// Arrange
		var t = TypeHelper.IsNumeric( typeof( string ) );

		// Assert
		t.Should().BeFalse();
	}

	[Fact]
	public void IsString_ForStringType_ReturnsTrue()
	{
		// Arrange
		var t = TypeHelper.IsString( typeof( string ) );

		// Assert
		t.Should().BeTrue();
	}

	[Fact]
	public void IsString_ForNumericType_ReturnsFalse()
	{
		// Arrange
		var t1 = TypeHelper.IsString( typeof( int ) );
		var t2 = TypeHelper.IsString( typeof( double ) );

		// Assert
		t1.Should().BeFalse();
		t2.Should().BeFalse();
	}

	[Fact]
	public void ConvertFromTo_AnyObject_ReturnsAppropriate()
	{
		// Arrange
		var t1 = TypeHelper.ConvertFromTo<int, double>( 0 );
		var t2 = TypeHelper.ConvertFromTo<int, double?>( 0 );

		// Assert
		t1.Should().Be( 0 ).And.BeOfType( typeof( double ) );
		t2.Should().Be( 0 ).And.BeOfType( typeof( double ) );
	}

	[Fact]
	public void ConvertFromTo_NullObject_ReturnsDefault()
	{
		// Arrange
		var t = TypeHelper.ConvertFromTo<object?, int>( null );

		// Assert
		t.Should().Be( 0 ).And.BeOfType( typeof( int ) );
	}

	[Fact]
	public void ConvertFromTo_EmptyString_ReturnsDefault()
	{
		// Arrange
		var t = TypeHelper.ConvertFromTo<string, int>( "" );

		// Assert
		t.Should().Be( 0 ).And.BeOfType( typeof( int ) );
	}
}