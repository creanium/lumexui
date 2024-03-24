// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;

namespace LumexUI;

public class LumexElement : LumexComponentBase
{
    /// <summary>
    /// Defines the content to be rendered inside the element.
    /// </summary>
    [Parameter] public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Defines a HTML tag of the element.
    /// </summary>
    [Parameter] public string Tag { get; set; } = "span";

    /// <summary>
    /// Defines an optional id attribute of the element.
    /// </summary>
    [Parameter] public string? Id { get; set; }

    protected override void BuildRenderTree( RenderTreeBuilder builder )
    {
        builder.OpenElement( 0, Tag );
        builder.AddAttribute( 1, "id", Id );
        builder.AddAttribute( 2, "class", RootClass );
        builder.AddAttribute( 3, "style", RootStyle );
        builder.AddMultipleAttributes( 4, AdditionalAttributes );
        builder.AddContent( 5, ChildContent );
        builder.CloseElement();
    }
}
