// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using Microsoft.Extensions.DependencyInjection;

using TailwindMerge;

namespace LumexUI.Variants.Extensions;

public static class ServiceCollectionExtensions
{
	/// <summary>
	/// 
	/// </summary>
	/// <param name="services"></param>
	public static void AddTwVariants( this IServiceCollection services )
	{
		services.AddScoped<TwMerge>();
		services.AddScoped<TwVariants>();
	}
}
