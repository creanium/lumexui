// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using System.Text.RegularExpressions;

namespace LumexUI.Docs.Client.Extensions;

internal static partial class StringExtensions
{
    internal static string[] SplitCamelCase( this string value )
    {
        return CamelCase().Split( value );
    }

    [GeneratedRegex( @"(?<!^)(?=[A-Z])" )]
    private static partial Regex CamelCase();
}
