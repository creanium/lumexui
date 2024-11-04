using System.ComponentModel;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace LumexUI.Infrastructure;

/// <summary>
/// A component that is used to move some of the components' body rendering 
/// to the end of the render queue so we can collect the list of child components first.
/// </summary>
/// <remarks>
/// For internal use only. Do not use.
/// </remarks>
[EditorBrowsable( EditorBrowsableState.Never )]
public class Defer : ComponentBase
{
    [Parameter] public RenderFragment? ChildContent { get; set; }

    /// <inheritdoc />
    protected override void BuildRenderTree( RenderTreeBuilder builder )
    {
        builder.AddContent( 0, ChildContent );
    }
}
