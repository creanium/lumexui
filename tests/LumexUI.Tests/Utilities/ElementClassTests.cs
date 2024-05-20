// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using LumexUI.Utilities;

namespace LumexUI.Tests.Utilities;

public class ElementClassTests
{
    [Fact]
    public void Empty_ShouldReturnEmptyValue()
    {
        var elementClass = ElementClass.Empty();

        var actual = elementClass.ToString();

        actual.Should().BeEmpty();
    }

    [Fact]
    public void Default_ShouldReturnCorrectValue()
    {
        var elementClass = ElementClass.Default( "item-one" );

        var actual = elementClass.ToString();

        actual.Should().Be( "item-one" );
    }

    [Fact]
    public void Add_Values_ShouldReturnCorrectValues()
    {
        var elementClass = new ElementClass( "item-one" )
            .Add( "item-two" )
            .Add( "item-three" );

        var actual = elementClass.ToString();

        actual.Should().Be( "item-one item-two item-three" );
    }

    [Fact]
    public void Add_NestedElementClass_ShouldReturnCorrectValues()
    {
        var elementClass = new ElementClass( "item-one" );
        var nestedElementClass = new ElementClass( "item-two" )
            .Add( "item-foo" );

        elementClass.Add( nestedElementClass );
        var actual = elementClass.ToString();

        actual.Should().Be( "item-one item-two item-foo" );
    }

    [Fact]
    public void Add_ConditionalValues_ShouldReturnCorrectValues()
    {
        var elementClass = new ElementClass( "item-one" )
            .Add( "item-two", when: false )
            .Add( "item-three", when: true );

        var actual = elementClass.ToString();

        actual.Should().Be( "item-one item-three" );
    }

    [Fact]
    public void Add_FuncConditionalValues_ShouldReturnCorrectValues()
    {
        var elementClass = new ElementClass( "item-one" )
            .Add( "item-two", when: () => false )
            .Add( "item-three", when: () => true );

        var actual = elementClass.ToString();

        actual.Should().Be( "item-one item-three" );
    }

    [Fact]
    public void Add_ConditionalNestedElementClass_ShouldReturnCorrectValues()
    {
        var elementClass = new ElementClass( "item-one" );
        var nestedElementClass = new ElementClass( "item-two" )
            .Add( "item-foo" );

        elementClass.Add( nestedElementClass, when: true );
        var actual = elementClass.ToString();

        actual.Should().Be( "item-one item-two item-foo" );
    }

    [Fact]
    public void Add_FuncConditionalNestedElementClass_ShouldReturnCorrectValues()
    {
        var elementClass = new ElementClass( "item-one" );
        var nestedElementClass = new ElementClass( "item-two" )
            .Add( "item-foo" );

        elementClass.Add( nestedElementClass, when: () => false );
        var actual = elementClass.ToString();

        actual.Should().Be( "item-one" );
    }

    [Fact]
    public void Add_FuncValues_ShouldReturnCorrectValues()
    {
        var elementClass = new ElementClass( "item-one" )
            .Add( () => "item-two", when: false )
            .Add( () => "item-three", when: true );

        var actual = elementClass.ToString();

        actual.Should().Be( "item-one item-three" );
    }

    [Fact]
    public void Add_FuncConditionalFuncValues_ShouldReturnCorrectValues()
    {
        var elementClass = new ElementClass( "item-one" )
            .Add( () => "item-two", when: () => false )
            .Add( () => "item-three", when: () => true );

        var actual = elementClass.ToString();

        actual.Should().Be( "item-one item-three" );
    }

    [Fact]
    public void Add_ValidAttributes_ShouldReturnCorrectValues()
    {
        var attributes = new Dictionary<string, object> { { "class", "my-custom-class" } };
        var elementClass = new ElementClass( "item-one" )
            .Add( attributes );

        var actual = elementClass.ToString();

        actual.Should().Be( "item-one my-custom-class" );
    }

    [Fact]
    public void Add_InvalidAttributes_ShouldReturnCorrectValues()
    {
        var attributes = new Dictionary<string, object> { { "style", "my-custom-class" } };
        var elementClass = new ElementClass( "item-one" )
            .Add( attributes );

        var actual = elementClass.ToString();

        actual.Should().Be( "item-one" );
    }

    [Fact]
    public void Add_NullAttributes_ShouldNotThrow()
    {
        Dictionary<string, object>? attributes = null;
        var elementClass = new ElementClass( "item-one" )
            .Add( attributes );

        var actual = elementClass.ToString();

        actual.Should().Be( "item-one" );
    }

    [Fact]
    public void Add_NullAttributeItem_ShouldNotThrow()
    {
        var attributes = new Dictionary<string, object> { { "class", null! } };
        var elementClass = new ElementClass( "item-one" )
            .Add( attributes );

        var actual = elementClass.ToString();

        actual.Should().Be( "item-one" );
    }

    [Fact]
    public void ToString_EmptyBuffer_ShouldReturnEmptyValue()
    {
        var elementClass = new ElementClass()
            .Add( "some-class", false );

        var actual = elementClass.ToString();

        actual.Should().BeEmpty();
    }
}