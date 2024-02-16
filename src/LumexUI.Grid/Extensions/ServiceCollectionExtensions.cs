// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using LumexUI.Grid.Services;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace LumexUI.Grid.Extensions;

public static class ServiceCollectionExtensions
{
	public static void AddLumexGridServices( this IServiceCollection services )
	{
		services.TryAddScoped<ICellFactory, CellFactory>();
		services.TryAddTransient<IJsApiService, JsApiService>();
		services.TryAddTransient<IGridNavigationService, GridNavigationService>();
	}
}
