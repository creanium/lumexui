using LumexUI.Common;

namespace LumexUI;

internal class DataGridContext<T>( LumexDataGrid<T> owner ) : IComponentContext<LumexDataGrid<T>>
{
    public LumexDataGrid<T> Owner { get; } = owner;
}
