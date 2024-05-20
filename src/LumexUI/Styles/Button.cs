// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using System.Diagnostics.CodeAnalysis;

using LumexUI.Common;
using LumexUI.Utilities;

namespace LumexUI.Styles;

[ExcludeFromCodeCoverage]
internal readonly record struct Button
{
    private readonly static string _base = ElementClass.Empty()
        .Add( "inline-flex" )
        .Add( "items-center" )
        .Add( "justify-center" )
        .Add( "appearance-none" )
        .Add( "outline-none" )
        .Add( "select-none" )
        .Add( "whitespace-nowrap" )
        .Add( "min-w-max" )
        .Add( "subpixel-antialiased" )
        .Add( "overflow-hidden" )
        .Add( "bg-blue-400" )
        .Add( "text-white" )
        .Add( "hover:opacity-80" )
        .ToString();

    private readonly static string _disabled = ElementClass.Empty()
        .Add( "opacity-disabled" )
        .Add( "pointer-events-none" )
        .ToString();

    private readonly static string _fullWidth = ElementClass.Empty()
        .Add( "w-full" )
        .ToString();

    private static string GetSizeStyles( Size size ) => size switch
    {
        Size.Small => "min-w-16 h-8 px-3 gap-2 text-sm rounded-lg",
        Size.Medium => "min-w-20 h-10 px-4 gap-2 text-base rounded-xl",
        Size.Large => "min-w-24 h-12 px-6 gap-2 text-lg rounded-xl",
        _ => throw new NotImplementedException()
    };

	private static string GetVariantStyles( Variant variant, ThemeColor color ) => variant switch
	{
		Variant.Solid => ColorVariants.Solid[color],
		Variant.Outlined => ColorVariants.Outlined[color],
		Variant.Flat => ColorVariants.Flat[color],
		Variant.Shadow => ColorVariants.Shadow[color],
		Variant.Ghost => ColorVariants.Ghost[color],
		Variant.Light => ColorVariants.Light[color],
		_ => throw new NotImplementedException()
	};

    public static string GetStyles( LumexButton button )
    {
        var styles = new ElementClass()
            .Add( _base )
            .Add( _disabled, when: button.Disabled )
            .Add( _fullWidth, when: button.FullWidth )
            .Add( GetSizeStyles( button.Size ) )
            .Add( GetVariantStyles( button.Variant, button.Color ) )
            .Add( button.Class )
            .ToString();

        return styles;
    }
}
