// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using LumexUI.Extensions;
using LumexUI.Utilities;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.JSInterop;

namespace LumexUI;

/// <summary>
/// Provides functionality for rendering elements that can be expanded.
/// </summary>
public sealed class LumexExpander : LumexComponentBase
{
    /// <summary>
    /// Defines the content to be rendered inside the expander.
    /// </summary>
    [Parameter] public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Indicates whether the expander component is currently in the expanded state.
    /// </summary>
    /// <remarks>Default value is <see langword="false"/></remarks>
    [Parameter] public bool Expanded { get; set; }

    /// <summary>
    /// Defines the event callback that is fired whenever expander's transition has ended.
    /// </summary>
    [Parameter] public EventCallback<bool> OnTransitionEnd { get; set; }

    protected override string RootClass =>
        new CssBuilder( "lumex-expander" )
            .AddClass( Constants.ComponentStates.Expanded, when: _state is ExpanderState.Expanded )
            .AddClass( Constants.ComponentStates.Expanding, when: _state is ExpanderState.Expanding )
            .AddClass( Constants.ComponentStates.Collapsing, when: _state is ExpanderState.Collapsing )
            .AddClass( base.RootClass )
        .Build();

    protected override string? RootStyle =>
        new StyleBuilder()
            .AddStyle( "height", $"{_elementHeight}px", when: _state is ExpanderState.Collapsing or ExpanderState.Expanding )
            .AddStyle( base.RootStyle )
        .NullIfEmpty();

    private int _elementHeight;
    private bool _expanded;
    private bool _heightUpdated = true;

    private ExpanderState _state;
    private ElementReference _expander;

    /// <inheritdoc />
    protected override void OnParametersSet()
    {
        // We don't want to do `UpdateHeightAsync` every time
        // the component gets (re)rendered (especially on the first render).
        if( _expanded == Expanded )
        {
            return;
        }

        _heightUpdated = false;
        _expanded = Expanded;
        _state = _expanded
            ? ExpanderState.Expanding
            : ExpanderState.Collapsing;
    }

    /// <inheritdoc />
    protected override async Task OnAfterRenderAsync( bool firstRender )
    {
        if( _heightUpdated )
        {
            return;
        }

        if( _state is ExpanderState.Expanding or ExpanderState.Collapsing )
        {
            await UpdateHeightAsync();
        }
    }

    /// <inheritdoc />
    protected override void BuildRenderTree( RenderTreeBuilder builder )
    {
        builder.OpenElement( 0, "div" );
        builder.AddAttribute( 1, "class", RootClass );
        builder.AddAttribute( 2, "style", RootStyle );
        builder.AddAttribute( 3, "ontransitionend", EventCallback.Factory.Create( this, HandleTransitionEndAsync ) );
        builder.AddMultipleAttributes( 4, AdditionalAttributes );
        builder.AddElementReferenceCapture( 5, elementReference => _expander = elementReference );
        builder.AddContent( 6, ChildContent );
        builder.CloseElement();
    }

    private async ValueTask UpdateHeightAsync()
    {
        try
        {
            _heightUpdated = true;
            _elementHeight = await _expander.GetScrollHeightAsync();

            if( _state is ExpanderState.Collapsing )
            {
                _elementHeight = 0;
            }

            StateHasChanged();
        }
        catch( Exception ex ) when( ex is JSDisconnectedException or TaskCanceledException )
        {
            _elementHeight = 0;
        }
    }

    private Task HandleTransitionEndAsync()
    {
        if( _state is ExpanderState.Expanding )
        {
            _state = ExpanderState.Expanded;
        }
        else if( _state is ExpanderState.Collapsing )
        {
            _state = ExpanderState.Collapsed;
        }

        return OnTransitionEnd.InvokeAsync( true );
    }

    private enum ExpanderState
    {
        Collapsed, Expanding, Expanded, Collapsing
    }
}
