// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using System.Text.RegularExpressions;

namespace LumexUI.Docs.Extensions;

internal static class StringExtensions
{
    internal static string[] SplitCamelCase( this string self )
    {
        return Regex.Split( self, @"(?<!^)(?=[A-Z])" );
    }
}
