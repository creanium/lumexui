// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using System.Collections;

using static LumexUI.Utilities.ColorUtils;

namespace LumexUI.Theme;

/// <summary>
/// Represents a color with various shades.
/// </summary>
public sealed class ColorScale : IEnumerable<KeyValuePair<string, string?>>
{
    private readonly Dictionary<string, string?> _colors;

    public string? this[string key]
	{
		get => _colors.TryGetValue( key, out string? color ) ? color : null;
		set => _colors[key] = value;
	}

	public ColorScale()
    {
        _colors = [];
    }

    public ColorScale( string color ) : this()
    {
        _colors["default"] = color;
    }

    public ColorScale( Dictionary<string, string?> colors )
    {
        _colors = colors;
        SetDefault( "500" );
    }

    public ColorScale SetDefault( string key )
    {
        if( _colors.TryGetValue( key, out string? value ) )
        {
            _colors["default"] = value;
            _colors["foreground"] = GetReadableColor( value );
        }

        return this;
    }

    public IEnumerator<KeyValuePair<string, string?>> GetEnumerator()
    {
        return _colors.GetEnumerator();
    }

    // To enable the usage of spread operator
    internal void Add( KeyValuePair<string, string?> item )
    {
        _colors.Add( item.Key, item.Value );
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
