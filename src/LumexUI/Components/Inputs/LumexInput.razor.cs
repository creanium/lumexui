// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using LumexUI.Common;
using LumexUI.Extensions;
using LumexUI.Utilities;

using Microsoft.AspNetCore.Components;

namespace LumexUI;

public partial class LumexInput : LumexInputBase<string>
{
	/// <summary>
	/// Defines an additional content needed for this <see cref="LumexInput"/>.
	/// For example, <see cref="LumexNumBox{TValue}"/> uses this to add spin buttons.
	/// </summary>
	[Parameter] public RenderFragment? InputContent { get; set; }

	/// <summary>
	/// Defines an additional content that will be rendered before this <see cref="LumexInput"/> input element.
	/// </summary>
	[Parameter] public RenderFragment? ContentStart { get; set; }

	///<summary>
	/// Defines an additional content that will be rendered after this <see cref="LumexInput"/> input element.
	///</summary>
	[Parameter] public RenderFragment? ContentEnd { get; set; }

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
	/// Specifies the hint text that will be rendered under the input element.
	/// </summary>
	[Parameter] public string? Hint { get; set; }

	/// <summary>
	/// Indicates whether the value of the input element will be updated on every keystroke.
	/// </summary>
	/// <remarks>Default value: False</remarks>
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
	[Parameter] public InputType Type { get; set; }

	/// <summary>
	/// Defines the <c>inputmode</c> attribute for this isntance of the input element.
	/// This allows a browser to display an appropriate virtual keyboard.
	/// </summary>
	/// <remarks>Default value is <see cref="InputMode.Text"/></remarks>
	[Parameter] public InputMode Mode { get; set; } = InputMode.Text;

	protected override string RootClass =>
		new CssBuilder( "lumex-input-field" )
			.AddClass( $"lumex-input-field--{Size.ToDescription()}", when: Size != Size.Medium )
			.AddClass( $"lumex-input-field--underline", when: Underline )
			.AddClass( Constants.ComponentStates.Dirty, when: !string.IsNullOrEmpty( CurrentValueText ) )
			.AddClass( base.RootClass )
		.Build();

	private string InputCssClass =>
		new CssBuilder( "lumex-input-field-control" )
		.Build();

	private string LabelCssClass =>
		new CssBuilder( "lumex-input-field-label" )
			.AddClass( "lumex-input-field-label--placeholder", when: LabelPlaceholder )
			.AddClass( "is-required", when: Required )
		.Build();

	public override ValueTask FocusAsync()
	{
		IsFocused = true;
		return Element.FocusAsync();
	}

	public override ValueTask BlurAsync()
	{
		IsFocused = false;
		return Element.BlurAsync();
	}

	private protected Task OnChangeAsync( ChangeEventArgs args )
	{
		if( Instant )
		{
			return Task.CompletedTask;
		}

		return base.SetCurrentValueTextAsync( (string?)args.Value );
	}

	private protected Task OnInputAsync( ChangeEventArgs args )
	{
		if( !Instant )
		{
			return Task.CompletedTask;
		}

		IsFocused = true;

		return base.SetCurrentValueTextAsync( (string?)args.Value );
	}
}