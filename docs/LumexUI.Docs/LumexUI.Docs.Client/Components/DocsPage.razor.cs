using Microsoft.AspNetCore.Components;

namespace LumexUI.Docs.Client.Components;

public partial class DocsPage
{
    [Parameter] public RenderFragment? ChildContent { get; set; }

    private readonly List<DocsPageSection> _collectedSections = [];

    internal void AddSection( DocsPageSection section )
    {
        _collectedSections.Add( section );
    }
}
