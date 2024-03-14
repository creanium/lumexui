// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

namespace LumexUI.Theme;

public record Borders : BaseScale
{
    public string Xs { get; set; }
    public string Xl { get; set; }
    public string Xxl { get; set; }
    internal string Full => "9999rem";

    public Borders()
    {
        Xs = ".25rem";
        Sm = ".375rem";
        Md = ".5rem";
        Lg = ".75rem";
        Xl = "1rem";
        Xxl = "2rem";
    }
}