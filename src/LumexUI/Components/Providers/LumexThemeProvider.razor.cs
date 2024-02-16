// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using System.Globalization;
using System.Text;

using LumexUI.Theme;
using LumexUI.Utilities;

using Microsoft.AspNetCore.Components;

namespace LumexUI;

public partial class LumexThemeProvider
{
	[Parameter] public LumexTheme Theme { get; set; } = new();

	private readonly CultureInfo _culture = CultureInfo.InvariantCulture;

	private const string Prefix = "--lumex";

	private string BuildTheme()
	{
		var builder = new StringBuilder();

		builder.AppendLine( ":root {" );
		GenerateCssVars( builder );
		builder.AppendLine( "}" );

		return builder.ToString();
	}

	private void GenerateCssVars( StringBuilder builder )
	{
		GenerateThemeCssVars( builder );
		GenerateFontCssVars( builder );
		GenerateBodyCssVars( builder );
		GenerateLinkCssVars( builder );
		GenerateBorderCssVars( builder );
		GenerateTransitionCssVars( builder );
	}

	private void GenerateThemeCssVars( StringBuilder builder )
	{
		Dictionary<string, Color> themeColors = new()
		{
			{ "primary", Theme.Palette.Primary },
			{ "secondary", Theme.Palette.Secondary },
			{ "tertiary", Theme.Palette.Tertiary },
			{ "success", Theme.Palette.Success },
			{ "warning", Theme.Palette.Warning },
			{ "danger", Theme.Palette.Danger },
			{ "info", Theme.Palette.Info }
		};

		foreach( var color in themeColors )
		{
			foreach( var (Shade, Value) in color.Value.GetShades() )
			{
				if( string.IsNullOrEmpty( Shade ) )
				{
					builder.AppendLine( $"{Prefix}-{color.Key}: {ColorUtils.FromHexToRgb( Value )};" );
				}
				else
				{
					builder.AppendLine( $"{Prefix}-{color.Key}-{Shade}: {ColorUtils.FromHexToRgb( Value )};" );
				}
			}
		}
	}

	private void GenerateFontCssVars( StringBuilder builder )
	{
		var fonts = GetTypographyFonts();

		builder.AppendLine( $"{Prefix}-font-sans-serif: {fonts[0]}" );
		builder.AppendLine( $"{Prefix}-font-monospace: {fonts[1]}" );
	}

	private void GenerateBodyCssVars( StringBuilder builder )
	{
		builder.AppendLine( $"{Prefix}-body-bg: {Theme.Palette.Background};" );
		builder.AppendLine( $"{Prefix}-body-bg-rgb: {ColorUtils.FromHexToRgb( Theme.Palette.Background )};" );
		builder.AppendLine( $"{Prefix}-body-color: {Theme.Palette.Foreground};" );
		builder.AppendLine( $"{Prefix}-body-accent-color: {ColorUtils.Tint( Theme.Palette.Foreground, .65 )};" );
		builder.AppendLine( $"{Prefix}-body-secondary-color: {ColorUtils.FromHexToRgbCss( Theme.Palette.Foreground, .75 )};" );
		builder.AppendLine( $"{Prefix}-body-tertiary-color: {ColorUtils.FromHexToRgbCss( Theme.Palette.Foreground, .6 )};" );
		builder.AppendLine( $"{Prefix}-body-font-family: var({Prefix}-font-sans-serif);" );
		builder.AppendLine( $"{Prefix}-body-font-size: {Theme.Typography.FontSize};" );
		builder.AppendLine( $"{Prefix}-body-font-weight: {Theme.Typography.FontWeight};" );
		builder.AppendLine( $"{Prefix}-body-line-height: {Theme.Typography.LineHeight};" );
	}

	private void GenerateLinkCssVars( StringBuilder builder )
	{
		builder.AppendLine( $"{Prefix}-link-color: {Theme.Palette.Primary.Default};" );
	}

	private void GenerateBorderCssVars( StringBuilder builder )
	{
		builder.AppendLine( $"{Prefix}-border-width: 1px;" );
		builder.AppendLine( $"{Prefix}-border-style: solid;" );
		builder.AppendLine( $"{Prefix}-border-color: {Theme.Borders.Color};" );
		builder.AppendLine( $"{Prefix}-border-sm: {Theme.Borders.Sm};" );
		builder.AppendLine( $"{Prefix}-border-md: {Theme.Borders.Md};" );
		builder.AppendLine( $"{Prefix}-border-lg: {Theme.Borders.Lg};" );
		builder.AppendLine( $"{Prefix}-border-xl: {Theme.Borders.Xl};" );
		builder.AppendLine( $"{Prefix}-border-2xl: {Theme.Borders.Xxl};" );
		builder.AppendLine( $"{Prefix}-border-full: 9999rem;" );
	}

	private void GenerateTransitionCssVars( StringBuilder builder )
	{
		builder.AppendLine( $"{Prefix}-transition-duration: 200ms;" );
		builder.AppendLine( $"{Prefix}-transition-timing: cubic-bezier(0.4, 0, 0.2, 1);" );
	}

	private string[] GetTypographyFonts()
	{
		string sansSerif = !string.IsNullOrEmpty( Theme.Typography.SansSerif )
			? $"{Theme.Typography.SansSerif},{Theme.Typography.DefaultSansSerif};"
			: $"{Theme.Typography.DefaultSansSerif};";

		string monospace = !string.IsNullOrEmpty( Theme.Typography.Monospace )
			? $"{Theme.Typography.Monospace},{Theme.Typography.DefaultMonospace};"
			: $"{Theme.Typography.DefaultMonospace};";

		return new string[] { sansSerif, monospace };
	}
}