// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using System.Diagnostics.CodeAnalysis;

using LumexUI.Common;
using LumexUI.Utilities;

namespace LumexUI.Styles;

[ExcludeFromCodeCoverage]
internal readonly record struct Divider
{
    private readonly static string _base = ElementClass.Empty()
        .Add( "bg-divider" )
        .Add( "border-none" )
        .ToString();

    private static string GetOrientationStyles( Orientation orientation ) => orientation switch
    {
        Orientation.Horizontal => "w-full h-px",
        Orientation.Vertical => "h-full w-px",
        _ => throw new NotImplementedException()
    };

    public static string GetStyles( LumexDivider divider )
    {
        var styles = new ElementClass()
            .Add( _base )
            .Add( GetOrientationStyles( divider.Orientation ) )
            .Add( divider.Class )
            .ToString();

        return styles;
    }
}
