// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using LumexUI.Docs.Data;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.JSInterop;

namespace LumexUI.Docs.Components;

public partial class DocsPageTableOfContents : IDisposable
{
    [Parameter] public IEnumerable<PageSectionInfo> Sections { get; set; }

    [Inject] private IJSRuntime JSRuntime { get; set; } = default!;
    [Inject] private NavigationManager NavigationManager { get; set; } = default!;

    private readonly RenderFragment _renderTableOfContents;

    public DocsPageTableOfContents()
    {
        _renderTableOfContents = RenderTableOfContents;

        Sections = new List<PageSectionInfo>();
    }

    protected override void OnInitialized()
    {
        NavigationManager.LocationChanged += OnLocationChanged;
    }

    protected override async Task OnAfterRenderAsync( bool firstRender )
    {
        if( firstRender )
        {
            await ScrollToSection();
        }
    }

    private async void OnLocationChanged( object? sender, LocationChangedEventArgs e )
    {
        await ScrollToSection();
    }

    private async Task ScrollToSection()
    {
        var uri = new Uri( NavigationManager.Uri, UriKind.Absolute );
        var fragment = uri.Fragment;

        if( fragment.StartsWith( '#' ) )
        {
            var elementId = fragment[1..];
            await JSRuntime.InvokeVoidAsync( "LumexDocs.scrollIntoView", elementId );
        }
    }

    void IDisposable.Dispose()
    {
        NavigationManager.LocationChanged -= OnLocationChanged;
    }
}
