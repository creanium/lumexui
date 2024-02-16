// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

namespace LumexUI.Docs.Data;

internal record DocsNavigation
{
    internal ICollection<NavigationCategory> Categories { get; }

    internal DocsNavigation()
    {
        Categories = new List<NavigationCategory>();
    }

    internal DocsNavigation AddCategory( NavigationCategory category )
    {
        Categories.Add( category );

        return this;
    }
}
