// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using LumexUI.Common;
using LumexUI.Styles;
using LumexUI.Utilities;

using Microsoft.AspNetCore.Components;

namespace LumexUI;

/// <summary>
/// A component that represents an item within the <see cref="LumexAccordion"/>.
/// </summary>
[CompositionComponent( typeof( LumexAccordion ) )]
public partial class LumexAccordionItem : LumexComponentBase,
	ISlotComponent<AccordionItemSlots>,
	IDisposable
{
	/// <summary>
	/// Gets or sets the content to render.
	/// </summary>
	[Parameter] public RenderFragment? ChildContent { get; set; }

	/// <summary>
	/// Gets or sets the content to render at the start.
	/// </summary>
	[Parameter] public RenderFragment? StartContent { get; set; }

	/// <summary>
	/// Gets or sets the content to render as the title.
	/// </summary>
	[Parameter] public RenderFragment? TitleContent { get; set; }

	/// <summary>
	/// Gets or sets the content to render as the subtitle.
	/// </summary>
	[Parameter] public RenderFragment? SubtitleContent { get; set; }

	/// <summary>
	/// Gets or sets the unique identifier for the accordion item.
	/// </summary>
	[Parameter, EditorRequired] public string Id { get; set; } = default!;

	/// <summary>
	/// Gets or sets the title.
	/// </summary>
	[Parameter] public string? Title { get; set; }

	/// <summary>
	/// Gets or sets the subtitle.
	/// </summary>
	[Parameter] public string? Subtitle { get; set; }

	/// <summary>
	/// Gets or sets the function that resolves the indicator icon.
	/// </summary>
	[Parameter] public IndicatorResolver Indicator { get; set; } = ( _ ) => Icons.Rounded.ChevronLeft;

	/// <summary>
	/// Gets or sets a value indicating whether the accordion item is disabled.
	/// </summary>
	[Parameter] public bool Disabled { get; set; }

	/// <summary>
	/// Gets or sets a value indicating whether the accordion item is expanded.
	/// </summary>
	[Parameter] public bool Expanded { get; set; }

	/// <summary>
	/// Gets or sets the callback invoked when the expanded state changes.
	/// </summary>
	[Parameter] public EventCallback<bool> ExpandedChanged { get; set; }

	/// <summary>
	/// Gets or sets the CSS classes for the accordion item slots.
	/// </summary>
	[Parameter] public AccordionItemSlots? Classes { get; set; }

	[CascadingParameter] internal AccordionContext Context { get; set; } = default!;

	private readonly Memoizer<AccordionItemSlots, SlotsDeps> _slotsMemo;
	private AccordionItemSlots _slots = default!;

	private bool _disposed;
	private bool _disabled;
	private bool _expanded;

	/// <summary>
	/// Initializes a new instance of the <see cref="LumexAccordionItem"/>.
	/// </summary>
	public LumexAccordionItem()
	{
		_slotsMemo = new Memoizer<AccordionItemSlots, SlotsDeps>();
	}

	/// <summary>
	/// Asynchronously expands the accordion item.
	/// </summary>
	/// <returns>A <see cref="Task"/> representing the asynchronous expand operation.</returns>
	public Task ExpandAsync()
	{
		return SetExpandedStateTo( true );
	}

	/// <summary>
	/// Asynchronously collapses the accordion item.
	/// </summary>
	/// <returns>A <see cref="Task"/> representing the asynchronous collapse operation.</returns>
	public Task CollapseAsync()
	{
		return SetExpandedStateTo( false );
	}

	internal bool GetExpandedState() => _expanded;

	internal bool GetDisabledState() => _disabled;

	/// <inheritdoc />
	protected override void OnInitialized()
	{
		ContextNullException.ThrowIfNull( Context, nameof( LumexAccordionItem ) );

		Context.Register( this );
	}

	/// <inheritdoc />
	protected override void OnParametersSet()
	{
		if( string.IsNullOrWhiteSpace( Id ) )
		{
			throw new InvalidOperationException(
				$"{GetType()} requires a non-null, non-empty, non-whitespace value for parameter '{nameof( Id )}'." );
		}

		if( !string.IsNullOrWhiteSpace( Title ) && TitleContent is not null )
		{
			throw new InvalidOperationException(
				$"{GetType()} can only accept one title content source from its parameters. " +
				$"Do not supply both '{nameof( Title )}' and '{nameof( TitleContent )}'." );
		}

		if( !string.IsNullOrWhiteSpace( Subtitle ) && SubtitleContent is not null )
		{
			throw new InvalidOperationException(
				$"{GetType()} can only accept one subtitle content source from its parameters. " +
				$"Do not supply both '{nameof( Subtitle )}' and '{nameof( SubtitleContent )}'." );
		}

		// Perform a re-building only if the dependencies have changed
		_slots = _slotsMemo.Memoize( GetSlots, new SlotsDeps(
			Disabled,
			Class,
			Classes,
			Context.Owner.ItemClasses
		) );

		_expanded = Expanded || Context.Owner.ExpandedItems.Contains( Id ) || Context.Owner.Expanded;
		_disabled = Disabled || Context.Owner.DisabledItems.Contains( Id ) || Context.Owner.Disabled;
	}

	private async Task ToggleExpansionAsync()
	{
		if( _disabled || Context.Owner.SelectionMode is SelectionMode.None )
		{
			return;
		}

		_expanded = !_expanded;

		await Context.ToggleExpansionAsync( this );
		await ExpandedChanged.InvokeAsync( _expanded );
	}

	private Task SetExpandedStateTo( bool expanded )
	{
		_expanded = expanded;
		StateHasChanged();

		return ExpandedChanged.InvokeAsync( _expanded );
	}

	private AccordionItemSlots GetSlots() =>
		AccordionItem.GetSlots( this );

	/// <inheritdoc />
	public void Dispose()
	{
		Dispose( disposing: true );
		GC.SuppressFinalize( this );
	}

	/// <inheritdoc cref="IDisposable.Dispose" />
	protected virtual void Dispose( bool disposing )
	{
		if( !_disposed )
		{
			if( disposing )
			{
				Context.Unregister( this );
			}

			_disposed = true;
		}
	}

	private readonly record struct SlotsDeps(
		bool Disabled,
		string? Class,
		AccordionItemSlots? Classes,
		AccordionItemSlots? ItemClasses
	);
}
