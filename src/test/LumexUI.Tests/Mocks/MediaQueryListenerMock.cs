// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using LumexUI.Services;

namespace LumexUI.Tests.Mocks;

internal class MediaQueryListenerMock : IMediaQueryListener
{
	private readonly TestContextBase _ctx;

	private Action? _cachedOnChangeCallback;

	public bool Matched { get; private set; }

	public MediaQueryListenerMock( TestContextBase ctx )
	{
		_ctx = ctx;
	}

	public ValueTask DisposeAsync()
	{
		return ValueTask.CompletedTask;
	}

	public ValueTask MatchAsync( string mediaQuery, Action onChange )
	{
		_cachedOnChangeCallback = onChange;

		return ValueTask.CompletedTask;
	}

	public void FakeMediaChangeEvent( bool matches )
	{
		Matched = matches;
		_ctx.Renderer.Dispatcher.InvokeAsync( () => _cachedOnChangeCallback?.Invoke() );
	}
}
