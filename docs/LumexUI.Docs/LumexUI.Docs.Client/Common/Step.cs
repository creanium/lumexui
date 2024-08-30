using Microsoft.AspNetCore.Components;

namespace LumexUI.Docs.Client.Common;

public record Step
{
    public string? Code { get; init; }
    public string? Title { get; init; }
    public RenderFragment? Body { get; init; }
}
