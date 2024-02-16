// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

namespace LumexUI.Docs.Extensions;

internal static class TypeExtensions
{
    internal static string GetNameWithoutGenericArity( this Type t )
    {
        string name = t.Name;
        int index = name.IndexOf( '`' );

        return index == -1 ? name : name[..index];
    }
}
