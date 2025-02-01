// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using LumexUI.Common;
using LumexUI.Styles;
using LumexUI.Utilities;

using Microsoft.AspNetCore.Components;

namespace LumexUI;

/// <summary>
/// A component that represents a checkbox input.
/// </summary>
public partial class LumexCheckbox : LumexBooleanInputBase, ISlotComponent<CheckboxSlots>
{
	/// <summary>
	/// Gets or sets the border radius of the checkbox.
	/// </summary>
	/// <remarks>
	/// The default is <see cref="Radius.Medium"/>
	/// </remarks>
	[Parameter] public Radius Radius { get; set; } = Radius.Medium;

	/// <summary>
	/// Gets or sets the icon to be used for indicating a checked state of the checkbox.
	/// </summary>
	[Parameter] public string? CheckIcon { get; set; }

	/// <summary>
	/// Gets or sets the CSS class names for the checkbox slots.
	/// </summary>
	[Parameter] public CheckboxSlots? Classes { get; set; }

	[CascadingParameter] internal CheckboxGroupContext? Context { get; set; }

	private readonly Memoizer<CheckboxSlots, SlotsDeps> _slotsMemo;
	private readonly RenderFragment _renderCheckIcon;

	private CheckboxSlots _slots = default!;

	/// <summary>
	/// Initializes a new instance of the <see cref="LumexCheckbox"/>.
	/// </summary>
	public LumexCheckbox()
	{
		_slotsMemo = new Memoizer<CheckboxSlots, SlotsDeps>();
		_renderCheckIcon = RenderCheckIcon;
	}

	/// <inheritdoc />
	public override async Task SetParametersAsync( ParameterView parameters )
	{
		await base.SetParametersAsync( parameters );

		Color = parameters.TryGetValue<ThemeColor>( nameof( Color ), out var color )
			? color
			: Context?.Owner.Color ?? ThemeColor.Primary;

		Size = parameters.TryGetValue<Size>( nameof( Size ), out var size )
			? size
			: Context?.Owner.Size ?? Size.Medium;

		if( parameters.TryGetValue<Radius>( nameof( Radius ), out var radius ) )
		{
			Radius = radius;
		}
		else if( Context is not null )
		{
			Radius = Context.Owner.Radius;
		}
	}

	/// <inheritdoc />
	protected override void OnParametersSet()
	{
		// Perform a re-building only if the dependencies have changed
		_slots = _slotsMemo.Memoize( GetSlots, new SlotsDeps(
			GetDisabledState(),
			Radius,
			Color,
			Size,
			Class,
			Classes
		) );
	}

	/// <inheritdoc />
	protected internal override bool GetDisabledState() =>
		Disabled || ( Context?.Owner.Disabled ?? false );

	/// <inheritdoc />
	protected internal override bool GetReadOnlyState() =>
		ReadOnly || ( Context?.Owner.ReadOnly ?? false );

	private CheckboxSlots GetSlots()
	{
		return Checkbox.GetStyles( this );
	}

	private readonly record struct SlotsDeps(
		bool Disabled,
		Radius Radius,
		ThemeColor Color,
		Size Size,
		string? Class,
		CheckboxSlots? Classes
	);
}
