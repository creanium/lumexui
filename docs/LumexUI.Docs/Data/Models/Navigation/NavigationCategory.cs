// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using LumexUI.Docs.Extensions;

namespace LumexUI.Docs.Data;

internal record NavigationCategory
{
    internal string Name { get; }

    internal ICollection<NavigationItem> Items { get; }

    internal IDictionary<string, NavigationCategory>? GroupedItems { get; private set; }

    internal bool Locked { get; private set; }

    internal NavigationCategory( string name )
    {
        Name = name;
        Items = new List<NavigationItem>();
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

    internal NavigationCategory AddGroup( NavigationCategory group )
    {
        GroupedItems ??= new Dictionary<string, NavigationCategory>();
        GroupedItems.Add( group.Name, group );

        return this;
    }

    internal NavigationCategory CopyFrom( NavigationCategory source )
    {
        if( source.GroupedItems is not null )
        {
            foreach( var group in source.GroupedItems )
            {
                foreach( var item in group.Value.Items )
                {
                    Items.Add( item );
                }
            }
        }
        else
        {
            foreach( var item in source.Items )
            {
                Items.Add( item );
            }
        }

        return this;
    }

    internal NavigationCategory Lock()
    {
        Locked = true;
        return this;
    }
}
