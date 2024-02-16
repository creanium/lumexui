// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using LumexUI.Common;

using Microsoft.AspNetCore.Components;

namespace LumexUI;

public partial class LumexTextBox : LumexInputBase<string>
{
	///<summary>
	/// Defines an additional content that will be rendered in the start of <see cref="LumexInput"/>.
	///</summary>
	[Parameter] public RenderFragment ContentStart { get; set; } = null!;

	///<summary>
	/// Defines an additional content that will be rendered in the end of <see cref="LumexInput"/>.
	///</summary>
	[Parameter] public RenderFragment ContentEnd { get; set; } = null!;

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
	[Parameter] public InputType Type { get; set; }

	/// <summary>
	/// Defines the <c>inputmode</c> attribute for this isntance of the input element.
	/// This allows a browser to display an appropriate virtual keyboard.
	/// </summary>
	/// <remarks>Default value is <see cref="InputMode.Text"/></remarks>
	[Parameter] public InputMode Mode { get; set; } = InputMode.Text;

	private LumexInput? _inputReference;

	public override ValueTask BlurAsync()
	{
		if( _inputReference == null )
		{
			return ValueTask.CompletedTask;
		}

		return _inputReference.BlurAsync();
	}
}