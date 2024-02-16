// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using Microsoft.JSInterop;

namespace LumexUI.Services;

/// <summary>
/// Provides a straightforward way to listen for and respond to changes in media query conditions.
/// </summary>
public class MediaQueryListener : IMediaQueryListener
{
	private readonly IJSRuntime _jsRuntime;
	private readonly DotNetObjectReference<MediaQueryListener> _selfReference;

	private bool _disposed;
	private string? _mediaQuery;
	private Action? _cachedOnChangeCallback;

	/// <inheritdoc />
	public bool Matched { get; private set; }

	/// <summary>
	/// Initializes a new instance of <see cref="MediaQueryListener" />.
	/// </summary>
	/// <param name="jsRuntime">The JavaScript runtime.</param>
	public MediaQueryListener( IJSRuntime jsRuntime )
	{
		_jsRuntime = jsRuntime;
		_selfReference = DotNetObjectReference.Create( this );
	}

	/// <inheritdoc />
	public ValueTask MatchAsync( string mediaQuery, Action onChange )
	{
		if( string.IsNullOrWhiteSpace( mediaQuery ) )
		{
			throw new ArgumentNullException( nameof( mediaQuery ), "Media query value cannot be null or empty." );
		}

		if( onChange is null )
		{
			throw new ArgumentNullException( nameof( onChange ), "onChange callback cannot be null." );
		}

		_mediaQuery = mediaQuery;
		_cachedOnChangeCallback = onChange;

		return _jsRuntime.InvokeVoidAsync( "Lumex.mediaQueryListener.matchMedia", _mediaQuery, _selfReference );
	}

	/// <summary>
	/// Invokes the cached callback.
	/// </summary>
	/// <param name="matches">A value indicating whether the document currently matches the specified media query.</param>
	[JSInvokable]
	public void MediaQueryChanged( bool matches )
	{
		if( _cachedOnChangeCallback is null )
		{
			throw new InvalidOperationException(
				$"{nameof( MediaQueryListener )} hasn't been configured correctly. " +
				$"Ensure {nameof( MatchAsync )} method is called before {nameof( MediaQueryChanged )}." );
		}

		Matched = matches;
		_cachedOnChangeCallback.Invoke();
	}

	/// <inheritdoc />
	public async ValueTask DisposeAsync()
	{
		await DisposeAsync( true );
		GC.SuppressFinalize( this );
	}

	protected virtual async ValueTask DisposeAsync( bool disposing )
	{
		if( _disposed )
			return;

		if( disposing )
		{
			await _jsRuntime.InvokeVoidAsync( "Lumex.mediaQueryListener.destroy" );

			_selfReference.Dispose();
			_cachedOnChangeCallback = null;
		}

		_disposed = true;
	}
}