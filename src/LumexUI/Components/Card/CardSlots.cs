using System.Diagnostics.CodeAnalysis;

using LumexUI.Common;

namespace LumexUI;

[ExcludeFromCodeCoverage]
public class CardSlots : ISlot
{
    public string? Root { get; set; }
    public string? Header { get; set; }
    public string? Body { get; set; }
    public string? Footer { get; set; }
}
