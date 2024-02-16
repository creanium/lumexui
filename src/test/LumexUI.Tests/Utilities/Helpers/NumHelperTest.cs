// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using LumexUI.Utilities;

namespace LumexUI.Tests.Utilities;

public class NumHelperTest
{
	[Theory]
	[InlineData( (sbyte)0 )]
	[InlineData( (byte)0 )]
	[InlineData( (short)0 )]
	[InlineData( (ushort)0 )]
	[InlineData( 0 )]
	[InlineData( (uint)0 )]
	[InlineData( (long)0 )]
	[InlineData( (ulong)0 )]
	[InlineData( 0.1F )]
	[InlineData( 0.1D )]
	public void From_AnyNumericType_ReturnsDouble<T>( T val )
	{
		NumHelper.From( val ).Should().BeOfType( typeof( double ) );
	}

	[Fact]
	public void From_Decimal_ReturnsDouble()
	{
		decimal val = 0.1M;

		NumHelper.From( val ).Should().BeOfType( typeof( double ) );
	}

	[Fact]
	public void To_AnyNumericType_ReturnsAppropriateTypes()
	{
		double val = 0.1;

		NumHelper.To<sbyte>( val ).Should().BeOfType( typeof( sbyte ) );
		NumHelper.To<byte>( val ).Should().BeOfType( typeof( byte ) );
		NumHelper.To<short>( val ).Should().BeOfType( typeof( short ) );
		NumHelper.To<ushort>( val ).Should().BeOfType( typeof( ushort ) );
		NumHelper.To<int>( val ).Should().BeOfType( typeof( int ) );
		NumHelper.To<uint>( val ).Should().BeOfType( typeof( uint ) );
		NumHelper.To<long>( val ).Should().BeOfType( typeof( long ) );
		NumHelper.To<ulong>( val ).Should().BeOfType( typeof( ulong ) );
		NumHelper.To<float>( val ).Should().BeOfType( typeof( float ) );
		NumHelper.To<double>( val ).Should().BeOfType( typeof( double ) );
		NumHelper.To<decimal>( val ).Should().BeOfType( typeof( decimal ) );
	}
}
