// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using System.Diagnostics.CodeAnalysis;

using LumexUI.Common;
using LumexUI.Utilities;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace LumexUI;

/// <summary>
/// 
/// </summary>
public partial class LumexAlert : LumexComponentBase, ISlotComponent<AlertSlots>
{
	/// <summary>
	/// 
	/// </summary>
	[Parameter] public RenderFragment? ChildContent { get; set; }

	/// <summary>
	/// 
	/// </summary>
	[Parameter] public RenderFragment? TitleContent { get; set; }

	/// <summary>
	/// 
	/// </summary>
	[Parameter] public RenderFragment? DescriptionContent { get; set; }

	/// <summary>
	/// 
	/// </summary>
	[Parameter] public RenderFragment? StartContent { get; set; }

	/// <summary>
	/// 
	/// </summary>
	[Parameter] public RenderFragment? EndContent { get; set; }

	/// <summary>
	/// 
	/// </summary>
	[Parameter] public string? Title { get; set; }

	/// <summary>
	/// 
	/// </summary>
	[Parameter] public string? Description { get; set; }

	/// <summary>
	/// 
	/// </summary>
	[Parameter] public string? Icon { get; set; }

	/// <summary>
	/// 
	/// </summary>
	/// <remarks>
	/// The default value is <see cref="Radius.Medium"/>.
	/// </remarks>
	[Parameter] public Radius Radius { get; set; } = Radius.Medium;

	/// <summary>
	/// 
	/// </summary>
	/// <remarks>
	/// The default value is <see cref="ThemeColor.Primary"/>.
	/// </remarks>
	[Parameter] public ThemeColor Color { get; set; } = ThemeColor.Default;

	/// <summary>
	/// 
	/// </summary>
	/// <remarks>
	/// The default value is <see cref="AlertVariant.Flat"/>.
	/// </remarks>
	[Parameter] public AlertVariant Variant { get; set; } = AlertVariant.Flat;

	/// <summary>
	/// Hides the icon wrapper and only renders the plain icon if <see cref="HideIcon"/> is false.
	/// </summary>
	[Parameter] public bool HideIconWrapper { get; set; }
	
	/// <summary>
	/// 
	/// </summary>
	[Parameter] public bool HideIcon { get; set; }

	/// <summary>
	/// 
	/// </summary>
	[Parameter] public bool Closeable { get; set; }

	/// <summary>
	/// 
	/// </summary>
	[Parameter] public EventCallback<MouseEventArgs> OnClose { get; set; }

	/// <summary>
	/// 
	/// </summary>
	[Parameter] public AlertSlots? Classes { get; set; }

	private bool HasTitle => TitleContent is not null || !string.IsNullOrEmpty( Title );
	private bool HasDescription => DescriptionContent is not null || !string.IsNullOrEmpty( Description );
	private string AlertIcon => Icon ?? _icons[Color];

	private readonly Dictionary<ThemeColor, string> _icons = new()
	{
		[ThemeColor.Default] = Icons.Rounded.Info,
		[ThemeColor.Primary] = Icons.Rounded.Info,
		[ThemeColor.Secondary] = Icons.Rounded.Info,
		[ThemeColor.Success] = Icons.Rounded.CheckCircle,
		[ThemeColor.Warning] = Icons.Rounded.GppMaybe,
		[ThemeColor.Danger] = Icons.Rounded.Report,
		[ThemeColor.Info] = Icons.Rounded.Info
	};

	private Dictionary<string, ComponentSlot> _slots = [];

	/// <inheritdoc />
	protected override void OnParametersSet()
	{
		var alertStyle = Styles.Alert.Style( TwMerge );
		_slots = alertStyle( new()
		{
			[nameof( Color )] = Color.ToString(),
			[nameof( Radius )] = Radius.ToString(),
			[nameof( Variant )] = Variant.ToString(),
			[nameof( HideIcon )] = HideIcon.ToString(),
			[nameof( Closeable )] = Closeable.ToString(),
			[nameof( Icon )] = AlertIcon
		} );
	}

	[ExcludeFromCodeCoverage]
	private string? GetStyles( string slot )
	{
		if( !_slots.TryGetValue( slot, out var styles ) )
		{
			throw new NotImplementedException($"{slot} slot is not implemented in the styles.");
		}

		return slot switch
		{
			nameof( AlertSlots.Base ) => styles( Classes?.Base, Class ),
			nameof( AlertSlots.MainWrapper ) => styles( Classes?.MainWrapper ),
			nameof( AlertSlots.IconWrapper ) => styles( Classes?.IconWrapper ),
			nameof( AlertSlots.Icon ) => styles( Classes?.Icon ),
			nameof( AlertSlots.Title ) => styles( Classes?.Title ),
			nameof( AlertSlots.Description ) => styles( Classes?.Description ),
			_ => throw new NotImplementedException($"{slot} slot is not implemented in the styles")
		};
	}
}