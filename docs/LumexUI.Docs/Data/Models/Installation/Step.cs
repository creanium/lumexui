// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using Microsoft.AspNetCore.Components;

namespace LumexUI.Docs.Data;

public record Step()
{
    public string? Title { get; init; }

    public string? CodeFile { get; init; }

    public string? FileName { get; init; }

    public RenderFragment? Body { get; init; }
}
