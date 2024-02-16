// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using LumexUI.Services;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace LumexUI.Extensions;

/// <summary>
/// Provides extension methods to configure LumexUI services on a <see cref="IServiceCollection"/>.
/// </summary>
public static class ServiceCollectionExtensions
{
	/// <summary>
	/// Registers all LumexUI common services into the Dependency Injection (DI) container.
	/// </summary>
	/// <param name="services">The <see cref="IServiceCollection"/>.</param>
	public static void AddLumexServices( this IServiceCollection services )
	{
		services.AddLumexMediaQueryListener();
		services.AddLumexDropdownService();
		services.AddLumexDrawerService();
	}

	/// <summary>
	/// Registers a scoped instance of the <see cref="IDropdownService"/> service.
	/// </summary>
	/// <param name="services">The <see cref="IServiceCollection"/>.</param>
	private static void AddLumexDropdownService( this IServiceCollection services )
	{
		services.TryAddScoped<IDropdownService, DropdownService>();
	}

	/// <summary>
	/// Registers a scoped instance of the <see cref="IDrawerService"/> service.
	/// </summary>
	/// <param name="services">The <see cref="IServiceCollection"/>.</param>
	private static void AddLumexDrawerService( this IServiceCollection services )
	{
		services.TryAddScoped<IDrawerService, DrawerService>();
	}

	/// <summary>
	/// Registers a transient instance of the <see cref="IMediaQueryListener"/> service and a 
	/// scoped instance of the <see cref="IMediaQueryListenerFactory"/> service.
	/// </summary>
	/// <param name="services">The <see cref="IServiceCollection"/>.</param>
	private static void AddLumexMediaQueryListener( this IServiceCollection services )
	{
		services.TryAddTransient<IMediaQueryListener, MediaQueryListener>();
		services.TryAddScoped<IMediaQueryListenerFactory, MediaQueryListenerFactory>();
	}
}
