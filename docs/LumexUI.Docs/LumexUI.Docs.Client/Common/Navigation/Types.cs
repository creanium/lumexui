using LumexUI.Docs.Client.Extensions;

namespace LumexUI.Docs.Client.Common;

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
        var item = new NavigationItem
        {
            Name = name,
            Link = name.ToLowerInvariant().Replace( " ", "-" )
        };

        Items.Add( item );
        return this;
    }

    internal NavigationCategory AddItem( Type component )
    {
        var formattedName = component.GetNameWithoutGenericArity().SplitCamelCase()[1..];
        var item = new NavigationItem()
        {
            Name = string.Join( " ", formattedName ),
            Link = string.Join( "-", formattedName ).ToLowerInvariant()
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
