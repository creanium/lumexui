// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

namespace LumexUI.Services;

/// <summary>
/// Provides convenience for managing and interacting with <see cref="LumexDrawer"/>.
/// </summary>
public interface IDrawerService
{
	/// <summary>
	/// Toggles the visibility state of a <see cref="LumexDrawer"/> identified by its identifier.
	/// </summary>
	/// <param name="id">The unique identifier of the drawer to be toggled.</param>
	void Toggle( string id );

	/// <summary>
	/// Registers a new instance of <see cref="LumexDrawer"/>.
	/// </summary>
	/// <param name="drawer">The <see cref="LumexDrawer"/> instance to be registered.</param>
	void Register( LumexDrawer drawer );

	/// <summary>
	/// Unregisters an existing instance of <see cref="LumexDrawer"/>.
	/// </summary>
	/// <param name="drawer">The <see cref="LumexDrawer"/> instance to be unregistered.</param>
	void Unregister( LumexDrawer drawer );
}
