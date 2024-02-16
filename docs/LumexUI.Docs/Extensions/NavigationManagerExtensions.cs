// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using Microsoft.AspNetCore.Components;

namespace LumexUI.Docs.Extensions;

internal static class NavigationManagerExtensions
{
    internal static string GetPageRelativePath( this NavigationManager self )
    {
        string relativePath = self.ToBaseRelativePath( self.Uri );
        string[] fragments = relativePath.Split( '/' );

        if( fragments.Any( x => x.Contains( '#' ) ) )
        {
            for( int i = 0; i < fragments.Length; i++ )
            {
                if( fragments[i].Contains( '#' ) )
                {
                    int hashPos = fragments[i].IndexOf( "#", StringComparison.Ordinal );
                    fragments[i] = fragments[i][..hashPos];
                }
            }

            return string.Join( "/", fragments );
        }
        else
        {
            return relativePath;
        }
    }
}
