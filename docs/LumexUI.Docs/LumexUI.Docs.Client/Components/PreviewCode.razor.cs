using LumexUI.Docs.Client.Common;
using LumexUI.Utilities;

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

using TailwindMerge;

namespace LumexUI.Docs.Client.Components;
public partial class PreviewCode
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter, EditorRequired] public CodeBlock Code { get; set; } = default!;
    [Parameter] public Slots? Classes { get; set; }

    [Inject] private TwMerge TwMerge { get; set; } = default!;

    private string? BackgroundClass =>
        TwMerge.Merge( ElementClass.Empty()
            .Add( _slots.Background )
            .Add( Classes?.Background )
            .ToString() );

    private string? PreviewWrapperClass =>
        TwMerge.Merge( ElementClass.Empty()
            .Add( _slots.PreviewWrapper )
            .Add( Classes?.PreviewWrapper )
            .ToString() );

    private string? PreviewClass => 
        TwMerge.Merge( ElementClass.Empty()
            .Add( _slots.Preview )
            .Add( Classes?.Preview )
            .ToString() );

    private string ToolbarClass => ElementClass.Empty()
        .Add( "p-2" )
        .Add( "border-t" )
        .Add( "border-foreground-950/5" )
        .Add( "border-b", when: _expanded )
        .Add( "rounded-b-xl", when: !_expanded )
        .ToString();

    private readonly string _id = Identifier.New();
    private readonly Slots _slots = new()
    {
        Preview = "relative flex flex-wrap items-center gap-4",
        PreviewWrapper = "relative p-8 rounded-t-xl bg-zinc-50 not-prose",
        Background = "absolute inset-0 bg-dots [mask-image:radial-gradient(#fff_0%,transparent_100%)]",
    };

    private bool _expanded;
    private bool _copied;

    private void Expand()
    {
        _expanded = !_expanded;
    }

    private async Task CopyToClipboard()
    {
        await JSRuntime.InvokeVoidAsync( "LumexDocs.copyToClipboard", _id );
        _copied = true;
        StateHasChanged();

        await Task.Delay( 2000 );
        _copied = false;
        StateHasChanged();
    }

    public class Slots
    {
        public string? Preview { get; init; }
        public string? PreviewWrapper { get; init; }
        public string? Background { get; init; }
    }
}
