﻿// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using System.Diagnostics.CodeAnalysis;

using LumexUI.Common;
using LumexUI.Utilities;

namespace LumexUI.Styles;

[ExcludeFromCodeCoverage]
internal class Link
{
	private readonly static string _base = ElementClass.Empty()
		.Add( "inline-flex" )
		.Add( "items-center" )
		.Add( "hover:opacity-hover" )
		.Add( "active:opacity-60" )
		.Add( "transition-opacity" )
		.ToString();

	private readonly static string _disabled = ElementClass.Empty()
		.Add( "opacity-disabled" )
		.Add( "pointer-events-none" )
		.ToString();

	private readonly static string _active = ElementClass.Empty()
		.Add( "data-[active=true]:font-semibold" )
		.ToString();

	private static ElementClass GetColorStyles( ThemeColor color )
	{
		return ElementClass.Empty()
			.Add( "text-default", when: color is ThemeColor.Default )
			.Add( "text-primary", when: color is ThemeColor.Primary )
			.Add( "text-secondary", when: color is ThemeColor.Secondary )
			.Add( "text-success", when: color is ThemeColor.Success )
			.Add( "text-warning", when: color is ThemeColor.Warning )
			.Add( "text-danger", when: color is ThemeColor.Danger )
			.Add( "text-info", when: color is ThemeColor.Info );
	}

	private static ElementClass GetUnderlineStyles( Underline underline )
	{
		return ElementClass.Empty()
			.Add( "no-underline", when: underline is Underline.None )
			.Add( "hover:underline", when: underline is Underline.Hover )
			.Add( "underline", when: underline is Underline.Always )
			.Add( "underline-offset-4", when: underline is not Underline.None );
	}

	public static string? GetStyles( LumexLink link )
	{
		var twMerge = link.TwMerge;

		return twMerge.Merge(
			ElementClass.Default( _base )
				.Add( _active )
				.Add( _disabled, when: link.Disabled )
				.Add( GetColorStyles( link.Color ) )
				.Add( GetUnderlineStyles( link.Underline ) )
				.Add( link.Class )
				.ToString() );
	}
}
