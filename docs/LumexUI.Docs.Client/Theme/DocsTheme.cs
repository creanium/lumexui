// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using LumexUI.Common;
using LumexUI.Theme;

namespace LumexUI.Docs.Client.Theme;

public record DocsTheme : LumexTheme
{
	public DocsTheme()
	{
		DefaultTheme = ThemeType.Light;

		// Typography
		var fontFamily = new FontFamily()
		{
			Sans = "Inter var",
			Mono = "Fira Code var"
		};

		// Light theme
		Light = new ThemeConfigLight()
		{
			Layout = new LayoutConfig()
			{
				FontFamily = fontFamily,
			}
		};
	}
}
