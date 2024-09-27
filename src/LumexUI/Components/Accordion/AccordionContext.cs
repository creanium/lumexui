using LumexUI.Common;

namespace LumexUI;

internal sealed class AccordionContext( LumexAccordion owner ) : IComponentContext<LumexAccordion>
{
    public readonly List<LumexAccordionItem> _items = [];

    public LumexAccordion Owner { get; } = owner;

    public void Register( LumexAccordionItem item )
    {
        _items.Add( item );
    }

    public void Unregister( LumexAccordionItem item )
    {
        _items.Remove( item );
    }

    public bool IsLast( LumexAccordionItem item )
    {
        return item == _items[^1];
    }

    public ValueTask ToggleExpansionAsync( LumexAccordionItem item )
    {
        if( Owner.SelectionMode is SelectionMode.Multiple )
        {
            return ValueTask.CompletedTask;
        }

        return CollapseAllButThisAsync( item );
    }

    private async ValueTask CollapseAllButThisAsync( LumexAccordionItem item )
    {
        foreach( var accordionItem in _items )
        {
            if( item == accordionItem || accordionItem.Disabled )
            {
                continue;
            }

            await accordionItem.CollapseAsync();
        }
    }
}
