namespace LumexUI.Docs.Common;

internal record Navigation
{
    private readonly List<NavigationCategory> _categories;

    internal IEnumerable<NavigationCategory> Categories => _categories.AsEnumerable();

    internal Navigation()
    {
        _categories = [];
    }

    internal Navigation Add( NavigationCategory category )
    {
        _categories.Add( category );
        return this;
    }
}

internal record NavigationCategory
{
    private readonly List<string> _items;

    internal string Name { get; }
    internal string Icon { get; }
    internal IEnumerable<string> Items => _items.AsEnumerable();

    internal NavigationCategory( string name, string icon )
    {
        _items = [];

        Name = name;
        Icon = icon;
    }

    internal NavigationCategory Add( string item )
    {
        _items.Add( item );
        return this;
    }
}
