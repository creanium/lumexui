// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using LumexUI.Docs.Services;
using LumexUI.Extensions;
using LumexUI.Grid.Extensions;

using Microsoft.Extensions.DependencyInjection;

namespace LumexUI.Docs.Extensions;

/// <summary>
/// Provides extension methods to configure LumexUI Docs services on a <see cref="IServiceCollection"/>.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers all LumexUI Docs services into the Dependency Injection (DI) container.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/>.</param>
    public static void AddLumexDocsServices( this IServiceCollection services )
    {
        // LumexUI Components & Grid Services
        services.AddLumexServices();
        services.AddLumexGridServices();

        // LumexUI Docs Services
        services.AddSingleton<INavigationService, NavigationService>();
    }
}
