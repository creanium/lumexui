// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using LumexUI.Common;

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace LumexUI;

public partial class LumexNumBox<TValue> : LumexInputBase<TValue>, IAsyncDisposable
{
	///<summary>
	/// Defines an additional content that will be rendered before this <see cref="LumexInput"/> input element.
	///</summary>
	[Parameter] public RenderFragment ContentStart { get; set; } = null!;

	///<summary>
	/// Defines an additional content that will be rendered after this <see cref="LumexInput"/> input element.
	///</summary>
	[Parameter] public RenderFragment ContentEnd { get; set; } = null!;

	/// <summary>
	/// Specifies whether the spin buttons are visible.
	/// 
	/// Default value: True
	/// </summary>
	[Parameter] public bool ShowArrows { get; set; } = true;

	/// <summary>
	/// Defines the placeholder for this instance of the input element.
	/// </summary>
	[Parameter] public string? Placeholder { get; set; }

	/// <summary>
	/// Indicates whether the label of the input element will act like a placeholder.
	/// If true, the label will act like a placeholder when input element is not focused.
	/// </summary>
	/// <remarks>Default value is <see langword="false"/></remarks>
	[Parameter] public bool LabelPlaceholder { get; set; }

	/// <summary>
	/// Defines whether the value of the input element will be updated on every keystroke.
	/// </summary>
	/// <remarks>Default value is <see langword="false"/></remarks>
	[Parameter] public bool Instant { get; set; }

	/// <summary>
	/// Indicates whether the underline will be shown when the input is focused.
	/// </summary>
	/// <remarks>Default value is <see langword="true"/></remarks>
	[Parameter] public bool Underline { get; set; } = true;

	/// <summary>
	/// Defines the type for this isntance of the input element.
	/// This is used to set different settings for the component.
	/// </summary>
	/// <remarks>Default value is <see cref="InputType.Text"/></remarks>
	[Parameter] public InputType Type { get; set; } = InputType.Text;

	/// <summary>
	/// Defines the <c>inputmode</c> attribute for this isntance of the input element.
	/// This allows a browser to display an appropriate virtual keyboard.
	/// </summary>
	/// <remarks>Default value is <see cref="InputMode.Numeric"/></remarks>
	[Parameter] public InputMode Mode { get; set; } = InputMode.Numeric;

	private LumexInput? _inputReference;

	private IJSObjectReference? _jsEventDisposable;

	protected override async Task OnAfterRenderAsync( bool firstRender )
	{
		if( firstRender )
		{
			//_jsEventDisposable = await JS.InvokeAsync<IJSObjectReference>( "", _inputReference!.Element );
		}

		await base.OnAfterRenderAsync( firstRender );
	}

	internal void Increment() => ChangeValue( factor: 1 );

	internal void Decrement() => ChangeValue( factor: -1 );

	private void ChangeValue( double factor )
	{
		TValue nextValue = (TValue)(object)Convert.ToDouble( (double?)(object?)Value + 1 * factor );

		ValueChanged.InvokeAsync( nextValue );
	}

	public async ValueTask DisposeAsync()
	{
		try
		{
			if( _jsEventDisposable is not null )
			{
				await _jsEventDisposable.InvokeVoidAsync( "destroy" );
				await _jsEventDisposable.DisposeAsync();
			}

			GC.SuppressFinalize( this );
		}
		catch( JSDisconnectedException )
		{
			// The JS side may routinely be gone already if the reason we're disposing is that
			// the client disconnected. This is not an error.
		}
	}
}