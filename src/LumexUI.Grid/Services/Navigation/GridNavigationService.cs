// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using Microsoft.AspNetCore.Components.Web;

namespace LumexUI.Grid.Services;

internal class GridNavigationService : IGridNavigationService
{
	private readonly Dictionary<string, Func<object?, ValueTask>> _keyUpMap = new();
	private readonly Dictionary<string, Func<object?, ValueTask>> _keyDownMap = new();

	public void RegisterAction( string key, Func<object?, ValueTask> action, bool onKeyUp )
	{
		if( onKeyUp )
		{
			RegisterActionCore( _keyUpMap, key, action );
		}
		else
		{
			RegisterActionCore( _keyDownMap, key, action );
		}
	}

	public ValueTask HandleKeyAsync( KeyboardEventArgs args, object? obj, bool onKeyUp )
	{
		if( onKeyUp )
		{
			return HandleKeyCoreAsync( args, _keyUpMap, obj );
		}
		else
		{
			return HandleKeyCoreAsync( args, _keyDownMap, obj );
		}
	}

	private ValueTask HandleKeyCoreAsync( KeyboardEventArgs args, Dictionary<string, Func<object?, ValueTask>> map, object? obj )
	{
		string key = string.Empty;

		if( args.ShiftKey )
		{
			key = KeyModifier.Shift;
		}

		key += args.Key;

		if( map.TryGetValue( key, out var action ) )
		{
			return action.Invoke( obj );
		}

		return ValueTask.CompletedTask;
	}

	private void RegisterActionCore( Dictionary<string, Func<object?, ValueTask>> map, string key, Func<object?, ValueTask> action )
	{
		if( !map.ContainsKey( key ) )
		{
			map[key] = action;
		}
		else
		{
			map[key] += action;
		}
	}
}