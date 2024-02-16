// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;

namespace LumexUI.Services;

/// <summary>
/// Represents a factory responsible for creating instances of the <see cref="IMediaQueryListener"/>.
/// </summary>
public class MediaQueryListenerFactory : IMediaQueryListenerFactory
{
	private readonly IServiceProvider _provider;

	/// <summary>
	/// Initializes a new instance of <see cref="MediaQueryListenerFactory" />.
	/// </summary>
	/// <param name="provider">The service provider.</param>
	public MediaQueryListenerFactory( IServiceProvider provider )
	{
		_provider = provider;
	}

	/// <inheritdoc />
	public IMediaQueryListener Create()
	{
		return new MediaQueryListener( _provider.GetRequiredService<IJSRuntime>() );
	}
}