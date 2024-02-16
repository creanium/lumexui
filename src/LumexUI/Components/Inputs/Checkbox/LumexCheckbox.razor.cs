// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using LumexUI.Utilities;

using Microsoft.AspNetCore.Components;

namespace LumexUI;

public partial class LumexCheckbox : LumexInputBase<bool>
{
	/// <summary>
	/// Specifies an optional CSS class names that will be applied to the checkbox.
	/// </summary>
	[Parameter]
	public string? CheckboxClass { get; set; }

	protected override string RootClass =>
		new CssBuilder( "lumex-checkbox" )
			.AddClass( base.RootClass )
		.Build();

	private string CheckboxCssClass =>
		CssBuilder.Empty()
			.AddClass( GetDefaultOrCustomCheckboxClass() )
		.Build();

	private protected async Task OnChange( ChangeEventArgs args )
	{
		CurrentValue = (bool)args.Value!;
	}

	private string GetDefaultOrCustomCheckboxClass()
		=> string.IsNullOrEmpty( CheckboxClass ) ? "lumex-checkbox-input" : CheckboxClass;
}