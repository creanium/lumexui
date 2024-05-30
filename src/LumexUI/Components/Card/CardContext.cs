using LumexUI.Common;

namespace LumexUI;

internal class CardContext( LumexCard owner ) : IComponentContext<LumexCard>
{
    public LumexCard Owner { get; } = owner;
}
