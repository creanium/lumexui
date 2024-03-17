// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using System.Text;

using LumexUI.Theme;
using LumexUI.Utilities;

using Microsoft.AspNetCore.Components;

using static LumexUI.Utilities.ColorUtils;

namespace LumexUI;

public partial class LumexThemeProvider
{
    [Parameter] public LumexThemeConfig Config { get; set; } = new();

    private const string Prefix = "lumex";

    private string BuildTheme( ThemeConfig theme )
    {
        var builder = new StringBuilder();
        var themeName = theme.Type.ToDescription();
        var cssSelector = $@"[data-theme=""{themeName}""]";

        if( themeName == Config.DefaultTheme.ToDescription() )
        {
            cssSelector = $":root, {cssSelector}";
        }

        builder.AppendLine( cssSelector + " {" );

        // Colors
        var themeColors = FlattenThemeColors( theme.Colors );

        foreach( var color in themeColors )
        {
            foreach( var scale in color.Value )
            {
                var scaleKey = scale.Key == "default" ? string.Empty : "-" + scale.Key;
                var scaleValue = !string.IsNullOrWhiteSpace( scale.Value ) ? HexToHsl( scale.Value ) : null;

                builder.AppendLine( $"--{Prefix}-{color.Key}{scaleKey}: {scaleValue};" );
            }
        }

        // Layout
        var themeLayout = theme.Layout;

        builder.AppendLine( $"--{Prefix}-body-bg: hsl(var(--{Prefix}-background));" );
        builder.AppendLine( $"--{Prefix}-body-color: hsl(var(--{Prefix}-foreground));" );
        builder.AppendLine( $"--{Prefix}-body-font-family: var(--{Prefix}-font-sans-serif);" );
        builder.AppendLine( $"--{Prefix}-body-font-size: var(--{Prefix}-font-size-md);" );
        builder.AppendLine( $"--{Prefix}-body-font-weight: var(--{Prefix}-font-weight);" );
        builder.AppendLine( $"--{Prefix}-body-line-height: var(--{Prefix}-line-height-md);" );

        builder.AppendLine( $"--{Prefix}-font-sans-serif: {Config.Typography.FontFamilies.SansSerif}, {FontFamily.DefaultSansSerif};" );
        builder.AppendLine( $"--{Prefix}-font-monospace: {Config.Typography.FontFamilies.Monospace}, {FontFamily.DefaultMonospace};" );
        builder.AppendLine( $"--{Prefix}-font-weight: {Config.Typography.FontWeight};" );
        builder.AppendLine( $"--{Prefix}-font-size-xs: {Config.Typography.FontSizes.Xs};" );
        builder.AppendLine( $"--{Prefix}-font-size-sm: {Config.Typography.FontSizes.Sm};" );
        builder.AppendLine( $"--{Prefix}-font-size-md: {Config.Typography.FontSizes.Md};" );
        builder.AppendLine( $"--{Prefix}-font-size-lg: {Config.Typography.FontSizes.Lg};" );
        builder.AppendLine( $"--{Prefix}-line-height-xs: {Config.Typography.LineHeights.Xs};" );
        builder.AppendLine( $"--{Prefix}-line-height-sm: {Config.Typography.LineHeights.Sm};" );
        builder.AppendLine( $"--{Prefix}-line-height-md: {Config.Typography.LineHeights.Md};" );
        builder.AppendLine( $"--{Prefix}-line-height-lg: {Config.Typography.LineHeights.Lg};" );

        builder.AppendLine( $"--{Prefix}-border-width: 1px;" );
        builder.AppendLine( $"--{Prefix}-border-style: solid;" );
        builder.AppendLine( $"--{Prefix}-border-color: {themeLayout.BorderColor};" );
        builder.AppendLine( $"--{Prefix}-border-xs: {Config.Borders.Xs};" );
        builder.AppendLine( $"--{Prefix}-border-sm: {Config.Borders.Sm};" );
        builder.AppendLine( $"--{Prefix}-border-md: {Config.Borders.Md};" );
        builder.AppendLine( $"--{Prefix}-border-lg: {Config.Borders.Lg};" );
        builder.AppendLine( $"--{Prefix}-border-xl: {Config.Borders.Xl};" );
        builder.AppendLine( $"--{Prefix}-border-xxl: {Config.Borders.Xxl};" );
        builder.AppendLine( $"--{Prefix}-border-full: {Config.Borders.Full};" );

        var emphasisColor = themeColors["foreground"]["900"];
        var linkColor = themeColors["primary"]["default"];

        emphasisColor = !string.IsNullOrWhiteSpace( emphasisColor ) ? HexToHsl( emphasisColor ) : null;
        linkColor = !string.IsNullOrWhiteSpace( linkColor ) ? HexToHsl( linkColor ) : null;

        builder.AppendLine( $"--{Prefix}-emphasis-color: {emphasisColor};" );
        builder.AppendLine( $"--{Prefix}-link-color: {linkColor};" );
        builder.AppendLine( $"--{Prefix}-hover-opacity: {themeLayout.HoverOpacity};" );
        builder.AppendLine( $"--{Prefix}-disabled-opacity: {themeLayout.DisabledOpacity};" );
        builder.AppendLine( $"--{Prefix}-shadow-color: 0 0% 0% / .1;" );

        builder.AppendLine( "}" );
        return builder.ToString();
    }

    private Dictionary<string, ColorScale> FlattenThemeColors( ThemeColors colors )
    {
        return new Dictionary<string, ColorScale>()
        {
            ["background"] = colors.Background,
            ["foreground"] = colors.Foreground,
            ["overlay"] = colors.Overlay,
            ["focus"] = colors.Focus,
            ["default"] = colors.Default,
            ["primary"] = colors.Primary,
            ["secondary"] = colors.Secondary,
            ["success"] = colors.Success,
            ["warning"] = colors.Warning,
            ["danger"] = colors.Danger,
            ["info"] = colors.Info,
        };
    }
}