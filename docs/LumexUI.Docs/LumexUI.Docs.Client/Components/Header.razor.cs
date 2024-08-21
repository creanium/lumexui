namespace LumexUI.Docs.Client.Components;

public partial class Header
{
    private readonly IEnumerable<(string Link, string Name)> _navigationItems = [
        new("docs/overview", "Docs"),
        new("docs/components", "Components"),
    ];
}
