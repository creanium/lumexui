using Microsoft.AspNetCore.Components;

namespace LumexUI.Docs.Client.Components;

public partial class DocsPage
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public RenderFragment? Description { get; set; }
    [Parameter] public string? Category { get; set; }
    [Parameter] public string? Title { get; set; }
    [Parameter] public bool ShowToc { get; set; } = true;

    private readonly List<DocsPageSection> _collectedSections = [];

    internal void AddSection( DocsPageSection section )
    {
        _collectedSections.Add( section );
    }
}
