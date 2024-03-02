// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using LumexUI.Utilities;

namespace LumexUI.Theme;

/// <summary>
/// Represents a color with various shades.
/// </summary>
public record ColorScale
{
    private static readonly int[] ScaleValues = [50, 100, 200, 300, 400, 500, 600, 700, 800, 900];

    public string S50 { get; }
    public string S100 { get; }
    public string S200 { get; }
    public string S300 { get; }
    public string S400 { get; }
    public string S500 { get; }
    public string S600 { get; }
    public string S700 { get; }
    public string S800 { get; }
    public string S900 { get; }
    public string Default { get; private set; }
    public string Contrast { get; private set; }

    public ColorScale(
            string s50,
            string s100,
            string s200,
            string s300,
            string s400,
            string s500,
            string s600,
            string s700,
            string s800,
            string s900
        )
    {
        S50 = s50;
        S100 = s100;
        S200 = s200;
        S300 = s300;
        S400 = s400;
        S500 = s500;
        S600 = s600;
        S700 = s700;
        S800 = s800;
        S900 = s900;
        Default = s500;
        Contrast = ColorUtils.Contrast( Default );
    }

    public ColorScale SetDefault( int scaleValue )
    {
        if( Array.IndexOf( ScaleValues, scaleValue ) == -1 )
        {
            throw new ArgumentException( $"Color scale value `{scaleValue}` is not recognised.", nameof( scaleValue ) );
        }

        foreach( var (Name, Value) in GetScale() )
        {
            if( int.TryParse( Name, out int parsedValue ) )
            {
                if( parsedValue == scaleValue )
                {
                    Default = Value;
                    Contrast = ColorUtils.Contrast( Default );
                    break;
                }
            }
        }

        return this;
    }

    internal (string Name, string Value)[] GetScale()
    {
        (string, string)[] values = [
            ("50", S50),
            ("100", S100),
            ("200", S200),
            ("300", S300),
            ("400", S400),
            ("500", S500),
            ("600", S600),
            ("700", S700),
            ("800", S800),
            ("900", S900),
            ("", Default),
            ("contrast", Contrast)
        ];

        return values;
    }
}
