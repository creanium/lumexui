// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using System.Globalization;

using LumexUI.Common;
using LumexUI.Utilities;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace LumexUI;

public abstract class LumexInputBase<TValue> : LumexComponentBase
{
	/// <summary>
	/// Specifies the id attribute of the input element. Used for label association.
	/// </summary>
	[Parameter] public int Id { get; set; }

	/// <summary>
	/// Specifies whether the input element should automatically receive focus.
	/// </summary>
	[Parameter] public bool Autofocus { get; set; }

	/// <summary>
	/// Specifies the label for this input element.
	/// </summary>
	[Parameter] public string? Label { get; set; }

	/// <summary>
	/// Indicates whether the input element is disabled.
	/// </summary>
	[Parameter] public bool Disabled { get; set; }

	/// <summary>
	/// Indicates whether the input element is required.
	/// </summary>
	[Parameter] public bool Required { get; set; }

	/// <summary>
	/// Indicates whether the input element is readonly.
	/// When true, the control will be immutable by user interaction.
	/// </summary>
	[Parameter] public bool ReadOnly { get; set; }

	/// <summary>
	/// Specifies the pattern attribute for this instance of the input element.
	/// </summary>
	[Parameter] public string? Pattern { get; set; }

	/// <summary>
	/// Specifies the size for this instance of the input element.
	/// </summary>
	/// <remarks>Default value: <see cref="Size.Medium"/></remarks>
	[Parameter] public Size Size { get; set; } = Size.Medium;

	/// <summary>
	/// Specifies the <see cref="CultureInfo"/> settings for this input.
	/// 
	/// Default value: InvariantCulture
	/// </summary>
	[Parameter] public CultureInfo? Culture { get; set; } = CultureInfo.InvariantCulture;

	/// <summary>
	/// Specifies the value of the input.
	/// </summary>
	[Parameter] public TValue? Value { get; set; }

	/// <summary>
	/// Specifies the callback that is fired when the value gets changed.
	/// </summary>
	[Parameter] public EventCallback<TValue> ValueChanged { get; set; }

	/// <summary>
	/// Specifies the callback that is fired when the input element loses focus.
	/// </summary>
	[Parameter] public EventCallback<FocusEventArgs> OnBlur { get; set; }

	/// <summary>
	/// Specifies the callback that is fired when the key is pressed within the input element.
	/// </summary>
	[Parameter] public EventCallback<KeyboardEventArgs> OnKeyDown { get; set; }

	/// <summary>
	/// Specifies the callback that is fired when the key is released within the input element.
	/// </summary>
	[Parameter] public EventCallback<KeyboardEventArgs> OnKeyUp { get; set; }

	[Inject] private protected IJSRuntime JS { get; set; } = default!;

	/// <summary>
	/// Specifies the associated <see cref="ElementReference"/>.
	/// </summary>
	public ElementReference Element { get; protected set; }

	/// <summary>
	/// Specifies the current value of the input.
	/// </summary>
	protected TValue? CurrentValue
	{
		get => Value;
		set
		{
			var hasChanged = !EqualityComparer<TValue>.Default.Equals( value, Value );
			if( hasChanged )
			{
				Value = value;
				ValueChanged.InvokeAsync( Value );
			}
		}
	}

	/// <summary>
	/// Specifies the current value of the input represented as a string.
	/// </summary>
	protected string? CurrentValueText
	{
		get => CurrentValue?.ToString();
		set
		{
			if( string.IsNullOrEmpty( value ) )
			{
				CurrentValue = default!;
			}
			else if( BindingConverter.TryConvertTo<TValue>( value, Culture, out var converted ) )
			{
				CurrentValue = converted;
			}
		}
	}

	private protected bool IsFocused { get; set; }

	public virtual ValueTask FocusAsync() { return new ValueTask(); }
	public virtual ValueTask BlurAsync() { return new ValueTask(); }

	protected override async Task OnAfterRenderAsync( bool firstRender )
	{
		// Auto-focus on first render
		if( firstRender && Autofocus )
		{
			await FocusAsync();
		}
	}

	private protected virtual Task SetCurrentValueTextAsync( string? value )
	{
		CurrentValueText = value;

		return Task.CompletedTask;
	}

	private protected virtual async Task InvokeOnBlurAsync( FocusEventArgs args )
	{
		IsFocused = false;

		await OnBlur.InvokeAsync( args );
	}

	private protected virtual async Task InvokeKeyDownAsync( KeyboardEventArgs args )
	{
		IsFocused = true;

		await OnKeyDown.InvokeAsync( args );
	}

	private protected virtual async Task InvokeKeyUpAsync( KeyboardEventArgs args )
	{
		IsFocused = true;

		await OnKeyUp.InvokeAsync( args );
	}
}
