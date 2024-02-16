// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using System.ComponentModel;

namespace LumexUI.Docs.Data;

public enum CalloutVariant
{
    [Description( "info" )]
    Info,

    [Description( "warning" )]
    Warning,

    [Description( "danger" )]
    Danger,

    [Description( "success" )]
    Success,

    [Description( "tip" )]
    Tip
}
