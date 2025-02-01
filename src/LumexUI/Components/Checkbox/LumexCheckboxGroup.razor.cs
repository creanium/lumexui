// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using LumexUI.Common;
using LumexUI.Styles;
using LumexUI.Utilities;

using Microsoft.AspNetCore.Components;

namespace LumexUI;

/// <summary>
/// A component that represents a group of checkboxes.
/// </summary>
public partial class LumexCheckboxGroup : LumexComponentBase, ISlotComponent<CheckboxGroupSlots>
{
	/// <summary>
	/// Gets or sets content to be rendered inside the checkbox group.
	/// </summary>
	[Parameter] public RenderFragment? ChildContent { get; set; }

	/// <summary>
	/// Gets or sets the label for the checkbox group.
	/// </summary>
	[Parameter] public string? Label { get; set; }

	/// <summary>
	/// Gets or sets the description for the checkbox group.
	/// </summary>
	[Parameter] public string? Description { get; set; }

	/// <summary>
	/// Gets or sets a value indicating whether the checkbox group is disabled.
	/// </summary>
	[Parameter] public bool Disabled { get; set; }

	/// <summary>
	/// Gets or sets a value indicating whether the checkbox group is read-only.
	/// </summary>
	[Parameter] public bool ReadOnly { get; set; }

	/// <summary>
	/// Gets or sets a color of the checkbox group.
	/// </summary>
	/// <remarks>
	/// The default is <see cref="ThemeColor.Primary"/>
	/// </remarks>
	[Parameter] public ThemeColor Color { get; set; } = ThemeColor.Primary;

	/// <summary>
	/// Gets or sets the border radius of the checkbox group.
	/// </summary>
	/// <remarks>
	/// The default is <see cref="Radius.Medium"/>
	/// </remarks>
	[Parameter] public Radius Radius { get; set; } = Radius.Medium;

	/// <summary>
	/// Gets or sets the size of the checkbox group.
	/// </summary>
	/// <remarks>
	/// The default value is <see cref="Size.Medium"/>
	/// </remarks>
	[Parameter] public Size Size { get; set; } = Size.Medium;

	/// <summary>
	/// Gets or sets the CSS class names for the checkbox group slots.
	/// </summary>
	[Parameter] public CheckboxGroupSlots? Classes { get; set; }

	/// <summary>
	/// Gets or sets the CSS class names for the checkboxes slots.
	/// </summary>
	[Parameter] public CheckboxSlots? CheckboxClasses { get; set; }

	private readonly CheckboxGroupContext _context;
	private readonly Memoizer<CheckboxGroupSlots, SlotsDeps> _slotsMemo;

	private CheckboxGroupSlots _slots = default!;

	/// <summary>
	/// Initializes a new instance of the <see cref="LumexCheckboxGroup"/>.
	/// </summary>
	public LumexCheckboxGroup()
	{
		_context = new CheckboxGroupContext( this );
		_slotsMemo = new Memoizer<CheckboxGroupSlots, SlotsDeps>();
	}

	/// <inheritdoc />
	protected override void OnParametersSet()
	{
		// Perform a re-building only if the dependencies have changed
		_slots = _slotsMemo.Memoize( GetSlots, new SlotsDeps(
			Class,
			Classes
		) );
	}

	private CheckboxGroupSlots GetSlots()
	{
		return CheckboxGroup.GetStyles( this );
	}

	private readonly record struct SlotsDeps(
		string? Class,
		CheckboxGroupSlots? Classes
	);
}