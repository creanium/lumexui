// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using System.ComponentModel;

namespace LumexUI.Common;

public enum Variant
{
    [Description( "solid" )]
    Solid,

    [Description( "outlined" )]
    Outlined,

    [Description( "flat" )]
    Flat,

    [Description( "shadow" )]
    Shadow,

    [Description( "ghost" )]
    Ghost,

    [Description( "light" )]
    Light
}