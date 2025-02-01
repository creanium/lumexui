// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

namespace LumexUI.Utilities;

public class Memoizer<TValue, TDeps> where TDeps : struct, IEquatable<TDeps>
{
	private bool _isMemoized;
	private Entry _entry;

	public TValue Value => _entry.Value;

	public TValue Memoize( Func<TValue> callback, TDeps dependencies )
	{
		// Check for a potential change
		if( _isMemoized && _entry.Dependencies.Equals( dependencies ) )
		{
			return _entry.Value;
		}

		// Compute and cache new value
		var value = callback();
		_entry = new Entry( value, dependencies );
		_isMemoized = true;

		return value;
	}

	private readonly struct Entry( TValue value, TDeps dependencies )
	{
		public readonly TValue Value = value;
		public readonly TDeps Dependencies = dependencies;
	}
}
