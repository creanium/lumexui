using LumexUI.Common;
using LumexUI.Components.DataGrid.Core;

namespace LumexUI;

internal class DataGridContext<T>( LumexDataGrid<T> owner ) : IComponentContext<LumexDataGrid<T>>
{
    public LumexDataGrid<T> Owner { get; } = owner;
    public EventCallbackSubscribable<object?> ColumnsFirstCollected { get; } = new();
}
