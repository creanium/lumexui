// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using LumexUI.Utilities;

namespace LumexUI.Tests.Utilities;

public class ElementStyleTests
{
    [Fact]
    public void Empty_ShouldReturnEmptyValue()
    {
        var elementStyle = ElementStyle.Empty();

        var actual = elementStyle.ToString();

        actual.Should().BeEmpty();
    }

    [Fact]
    public void Default_ValidProperty_ShouldReturnCorrectValue()
    {
        var elementStyle = ElementStyle.Default( "background-color", "DodgerBlue" );

        var actual = elementStyle.ToString();

        actual.Should().Be( "background-color:DodgerBlue;" );
    }

    [Theory]
    [InlineData( "" )]
    [InlineData( null )]
    [InlineData( "   " )]
    public void Default_InvalidProperty_ShouldThrowArgumentNull( string? property )
    {
        var action = () => ElementStyle.Default( property!, "DodgerBlue" );

        action.Should().Throw<ArgumentNullException>();
    }

    [Theory]
    [InlineData( "" )]
    [InlineData( null )]
    [InlineData( "   " )]
    public void Add_InvalidProperty_ShouldThrowArgumentNull( string? property )
    {
        var action = () => new ElementStyle().Add( property!, "DodgerBlue" );

        action.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void Add_Values_ShouldReturnCorrectValues()
    {
        var elementStyle = new ElementStyle( "background-color", "DodgerBlue" )
            .Add( "border-width", "1px 1px 1px 1px" )
            .Add( "padding", "35px" );

        var actual = elementStyle.ToString();

        actual.Should().Be( "background-color:DodgerBlue;border-width:1px 1px 1px 1px;padding:35px;" );
    }

    [Fact]
    public void Add_FuncValues_ShouldReturnCorrectValues()
    {
        var elementStyle = new ElementStyle( "background-color", "DodgerBlue" )
            .Add( "border-width", () => "1px 1px 1px 1px", when: false )
            .Add( "padding", () => "35px", when: true );

        var actual = elementStyle.ToString();

        actual.Should().Be( "background-color:DodgerBlue;padding:35px;" );
    }

    [Fact]
    public void Add_NestedElementStyle_ShouldReturnCorrectValues()
    {
        var elementStyle = new ElementStyle( "background-color", "DodgerBlue" );
        var nestedElementStyle = new ElementStyle( "border-width", "1px 1px 1px 1px" )
            .Add( "padding", "35px" );

        elementStyle.Add( nestedElementStyle );
        var actual = elementStyle.ToString();

        actual.Should().Be( "background-color:DodgerBlue;border-width:1px 1px 1px 1px;padding:35px;" );
    }

    [Fact]
    public void Add_ConditionalValues_ShouldReturnCorrectValues()
    {
        var elementStyle = new ElementStyle( "background-color", "DodgerBlue" )
            .Add( "border-width", "1px 1px 1px 1px", when: true )
            .Add( "padding", "35px", when: false );

        var actual = elementStyle.ToString();

        actual.Should().Be( "background-color:DodgerBlue;border-width:1px 1px 1px 1px;" );
    }

    [Fact]
    public void Add_FuncConditionalValues_ShouldReturnCorrectValues()
    {
        var elementStyle = new ElementStyle( "background-color", "DodgerBlue" )
            .Add( "border-width", "1px 1px 1px 1px", when: () => true )
            .Add( "padding", "35px", when: () => false );

        var actual = elementStyle.ToString();

        actual.Should().Be( "background-color:DodgerBlue;border-width:1px 1px 1px 1px;" );
    }

    [Fact]
    public void Add_FuncConditionalFuncValues_ShouldReturnCorrectValues()
    {
        var elementStyle = new ElementStyle( "background-color", "DodgerBlue" )
            .Add( "border-width", () => "1px 1px 1px 1px", when: () => true )
            .Add( "padding", () => "35px", when: () => false );

        var actual = elementStyle.ToString();

        actual.Should().Be( "background-color:DodgerBlue;border-width:1px 1px 1px 1px;" );
    }

    [Fact]
    public void Add_ConditionalNestedElementStyle_ShouldReturnCorrectValues()
    {
        var elementStyle = new ElementStyle( "background-color", "DodgerBlue" );
        var nestedElementStyle = new ElementStyle( "border-width", "1px 1px 1px 1px" )
            .Add( "padding", "35px" );

        elementStyle.Add( nestedElementStyle, when: true );
        var actual = elementStyle.ToString();

        actual.Should().Be( "background-color:DodgerBlue;border-width:1px 1px 1px 1px;padding:35px;" );
    }

    [Fact]
    public void Add_FuncConditionalNestedElementStyle_ShouldReturnCorrectValues()
    {
        var elementStyle = new ElementStyle( "background-color", "DodgerBlue" );
        var nestedElementStyle = new ElementStyle( "border-width", "1px 1px 1px 1px" )
            .Add( "padding", "35px" );

        elementStyle.Add( nestedElementStyle, when: () => false );
        var actual = elementStyle.ToString();

        actual.Should().Be( "background-color:DodgerBlue;" );
    }

    [Fact]
    public void Add_ValidAttributes_ShouldReturnCorrectValues()
    {
        var attributes = new Dictionary<string, object> { { "style", "border-width:1px 1px 1px 1px;" } };
        var elementStyle = new ElementStyle( "background-color", "DodgerBlue" )
            .Add( attributes );

        var actual = elementStyle.ToString();

        actual.Should().Be( "background-color:DodgerBlue;border-width:1px 1px 1px 1px;" );
    }

    [Fact]
    public void Add_InvalidAttributes_ShouldReturnCorrectValues()
    {
        var attributes = new Dictionary<string, object> { { "class", "border-width:1px 1px 1px 1px;" } };
        var elementStyle = new ElementStyle( "background-color", "DodgerBlue" )
            .Add( attributes );

        var actual = elementStyle.ToString();

        actual.Should().Be( "background-color:DodgerBlue;" );
    }

    [Fact]
    public void Add_NullAttributes_ShouldNotThrow()
    {
        Dictionary<string, object>? attributes = null;
        var elementStyle = new ElementStyle( "background-color", "DodgerBlue" )
            .Add( attributes );

        var actual = elementStyle.ToString();

        actual.Should().Be( "background-color:DodgerBlue;" );
    }

    [Fact]
    public void Add_NullAttributeItem_ShouldNotThrow()
    {
        var attributes = new Dictionary<string, object> { { "style", null! } };
        var elementStyle = new ElementStyle( "background-color", "DodgerBlue" )
            .Add( attributes );

        var actual = elementStyle.ToString();

        actual.Should().Be( "background-color:DodgerBlue;" );
    }

    [Fact]
    public void ToString_EmptyBuffer_ShouldReturnEmptyValue()
    {
        var elementStyle = new ElementStyle()
            .Add( "background-color", "DodgerBlue", when: false );

        var actual = elementStyle.ToString();

        actual.Should().BeEmpty();
    }
}