using LumexUI.Docs.Extensions;

namespace LumexUI.Docs.Common;

internal record Navigation
{
    internal List<NavigationCategory> Categories { get; }

    internal Navigation()
    {
        Categories = [];
    }

    internal Navigation AddCategory( NavigationCategory category )
    {
        Categories.Add( category );
        return this;
    }
}

internal record NavigationCategory
{
    internal string Name { get; }
    internal string Icon { get; }
    internal List<NavigationItem> Items { get; }

    internal NavigationCategory( string name, string icon )
    {
        Name = name;
        Icon = icon;
        Items = [];
    }

    internal NavigationCategory AddItem( string name )
    {
        var formattedName = name.Replace( "Lumex", "" ).SplitPascalCase();
        var item = new NavigationItem
        {
            Name = formattedName,
            Link = formattedName.ToKebabCase()
        };

        Items.Add( item );
        return this;
    }

    internal NavigationCategory CopyFrom( NavigationCategory source )
    {
        foreach( var item in source.Items )
        {
            Items.Add( item );
        }

        return this;
    }
}

internal record NavigationItem
{
    internal string? Name { get; init; }
    internal string? Link { get; init; }
}
