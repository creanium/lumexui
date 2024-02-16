// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using LumexUI.Common;
using LumexUI.Services;
using LumexUI.Utilities;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;

namespace LumexUI;

/// <summary>
/// A component that slides in and out from the edge of the screen.
/// <para>Commonly used to provide additional navigation or content options without taking up the entire screen space.</para>
/// </summary>
public partial class LumexDrawer : LumexComponentBase, IDisposable
{
	/// <summary>
	/// Defines the content to be rendered inside the drawer header.
	/// </summary>
	[Parameter] public RenderFragment? HeaderContent { get; set; }

	/// <summary>
	/// Defines the content to be rendered inside the drawer body.
	/// </summary>
	[Parameter] public RenderFragment? BodyContent { get; set; }

	/// <summary>
	/// Defines the unique identifier associated with the drawer.
	/// </summary>
	/// <remarks>Default value is <see cref="string.Empty"/></remarks>
	[Parameter, EditorRequired] public string Id { get; set; } = string.Empty;

	/// <summary>
	/// Indicates whether the header content will be shown.
	/// If false, the content will be visible only when the <see cref="LumexDrawer"/> is opened, that is on smaller screens.
	/// </summary>
	/// <remarks>Default value is <see langword="false"/></remarks>
	[Parameter] public bool ShowHeader { get; set; }

	/// <summary>
	/// Defines the position to which the <see cref="LumexDrawer"/> will be anchored when opening.
	/// </summary>
	/// <remarks>Default value is <see cref="Anchor.Start"/></remarks>
	[Parameter] public Anchor Anchor { get; set; }

	/// <summary>
	/// Defines the breakpoint at which the <see cref="LumexDrawer"/> will become toggleable.
	/// </summary>
	/// <remarks>Default value is <see cref="Breakpoint.Xs"/></remarks>
	[Parameter] public Breakpoint Breakpoint { get; set; }

	[Inject] private IDrawerService DrawerService { get; set; } = default!;
	[Inject] private NavigationManager NavigationManager { get; set; } = default!;

	protected override string RootClass =>
		new CssBuilder()
			.AddClass( "lumex-drawer", when: Breakpoint is Breakpoint.Xs )
			.AddClass( $"lumex-drawer--{Anchor.ToDescription()}", when: Breakpoint is Breakpoint.Xs )
			.AddClass( $"lumex-{Breakpoint.ToDescription()}:drawer", when: Breakpoint is not Breakpoint.Xs )
			.AddClass( $"lumex-{Breakpoint.ToDescription()}:drawer--{Anchor.ToDescription()}", when: Breakpoint is not Breakpoint.Xs )
			.AddClass( Constants.ComponentStates.Shown, when: _state is DrawerState.Shown )
			.AddClass( Constants.ComponentStates.Showing, when: _state is DrawerState.Showing )
			.AddClass( Constants.ComponentStates.Hiding, when: _state is DrawerState.Hiding )
			.AddClass( base.RootClass )
		.Build();

	private string HeaderCssClass =>
		GetCssToHide( "lumex-drawer-header" ).Build();

	private string DividerCssClass =>
		GetCssToHide( "lumex-drawer-divider" ).Build();

	private DrawerState _state;

	/// <summary>
	/// Toggles the visibility state of the <see cref="LumexDrawer"/>.
	/// </summary>
	public void Toggle()
	{
		if( _state is DrawerState.Shown )
		{
			Close();
		}
		else
		{
			Open();
		}

		StateHasChanged();
	}

	/// <inheritdoc />
	protected override void OnInitialized()
	{
		DrawerService.Register( this );
		NavigationManager.LocationChanged += OnLocationChanged;
	}

	private void OnLocationChanged( object? sender, LocationChangedEventArgs e )
	{
		Close();
		StateHasChanged();
	}

	private void Open()
	{
		_state = DrawerState.Showing;
	}

	private void Close()
	{
		_state = DrawerState.Hiding;
	}

	private void HandleTransitionEnd()
	{
		if( _state is DrawerState.Showing )
		{
			_state = DrawerState.Shown;
		}
		else if( _state is DrawerState.Hiding )
		{
			_state = DrawerState.Hidden;
		}
	}

	private CssBuilder GetCssToHide( string element )
	{
		return new CssBuilder( element )
					.AddClass( $"hidden", when: !ShowHeader && Breakpoint is Breakpoint.Xs )
					.AddClass( $"{Breakpoint.ToDescription()}:hidden", when: !ShowHeader && Breakpoint is not Breakpoint.Xs );
	}

	public void Dispose()
	{
		DrawerService.Unregister( this );
		NavigationManager.LocationChanged -= OnLocationChanged;
	}

	private enum DrawerState
	{
		Hidden, Showing, Shown, Hiding
	}
}