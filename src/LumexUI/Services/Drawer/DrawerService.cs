// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

namespace LumexUI.Services;

/// <summary>
/// Provides convenience for managing and interacting with <see cref="LumexDrawer"/>.
/// </summary>
public class DrawerService : IDrawerService
{
	private Dictionary<string, LumexDrawer> _drawers;

	/// <summary>
	/// Initializes a new instance of <see cref="DrawerService" />.
	/// </summary>
	public DrawerService()
	{
		_drawers = new();
	}

	/// <inheritdoc />
	public void Toggle( string id )
	{
		if( _drawers.TryGetValue( id, out var drawer ) )
		{
			drawer.Toggle();
		}
	}

	/// <inheritdoc />
	public void Register( LumexDrawer drawer )
	{
		if( !_drawers.ContainsKey( drawer.Id ) )
		{
			_drawers.Add( drawer.Id, drawer );
		}
		else
		{
			throw new InvalidOperationException(
				$"A drawer with the specified Id `{drawer.Id}` has already been registered." );
		}
	}

	/// <inheritdoc />
	public void Unregister( LumexDrawer drawer )
	{
		_drawers.Remove( drawer.Id );
	}
}
