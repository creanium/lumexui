using System.Runtime.CompilerServices;

using LumexUI.Utilities;

using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Routing;

namespace LumexUI.Infrastructure;

/// <summary>
/// An internal component representing the wrapper around the native <see cref="NavLink"/>.
/// </summary>
public class InternalNavLink : NavLink
{
    private bool _isActive;

    /// <summary>
    /// Initializes a new instance of the <see cref="InternalNavLink"/>.
    /// </summary>
    public InternalNavLink()
    {
        ActiveClass = string.Empty;
    }

    /// <inheritdoc/>
    protected override void BuildRenderTree( RenderTreeBuilder builder )
    {
        _isActive = GetActiveState( this );

        builder.OpenElement( 0, "a" );
        builder.AddMultipleAttributes( 1, AdditionalAttributes );

        if( _isActive )
        {
            builder.AddAttribute( 2, "data-active", Utils.GetDataAttributeValue( _isActive ) );
            builder.AddAttribute( 3, "aria-current", "page" );
        }

        builder.AddContent( 4, ChildContent );
        builder.CloseElement();
    }

    [UnsafeAccessor( UnsafeAccessorKind.Field, Name = "_isActive" )]
    private static extern ref bool GetActiveState( NavLink navLink );
}
