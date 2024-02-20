// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using LumexUI.Utilities;

using Microsoft.AspNetCore.Components;

namespace LumexUI;

public abstract class LumexComponentBase : ComponentBase
{
	/// <summary>
	/// Specifies a collection of additional attributes that will be applied to the component.
	/// </summary>
	[Parameter( CaptureUnmatchedValues = true )]
	public IReadOnlyDictionary<string, object>? AdditionalAttributes { get; set; } = new Dictionary<string, object>();

	/// <summary>
	/// Specifies an optional CSS class names that will be applied to the component.
	/// </summary>
	[Parameter] public string? Class { get; set; }

	/// <summary>
	/// Specifies an optional in-line styles that will be applied to the component.
	/// </summary>
	[Parameter] public string? Style { get; set; }

	internal string Name => GetType().Name;

	protected virtual string? RootClass =>
		CssBuilder.Empty()
			.AddClass( Class )
		.NullIfEmpty();

	protected virtual string? RootStyle =>
		StyleBuilder.Empty()
			.AddStyle( Style )
		.NullIfEmpty();
}
