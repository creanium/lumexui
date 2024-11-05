using LumexUI.Common;

namespace LumexUI;

/// <summary>
/// A component representing a <see cref="LumexDataGrid{T}"/> column whose cells display a checkbox.
/// </summary>
/// <remarks>
/// For internal use only.
/// </remarks>
/// <typeparam name="T">The type of data represented by each row in the grid.</typeparam>
public partial class _CheckboxColumn<T> : LumexColumnBase<T>
{
    private readonly Func<T, bool, Task> _selectItemDelegate;
    private readonly Func<T, bool> _checkItemSelectedDelegate;

    /// <summary>
    /// Initializes a new instance of the <see cref="_CheckboxColumn{T}"/>.
    /// </summary>
    public _CheckboxColumn()
    {
        _selectItemDelegate = SelectItemAsync;
        _checkItemSelectedDelegate = IsItemSelected;
    }

    private async Task SelectAllItemsAsync( bool selected )
    {
        var selectedItems = DataGrid.SelectedItems;
        if( selected )
        {
            if( DataGrid.Data is not null )
            {
                selectedItems = new HashSet<T>( DataGrid.Data );
            }
        }
        else
        {
            selectedItems.Clear();
        }

        await DataGrid.SelectedItemsChanged.InvokeAsync( selectedItems );
        DataGrid.Refresh();
    }

    private async Task SelectItemAsync( T item, bool selected )
    {
        var selectedItems = DataGrid.SelectedItems;
        if( DataGrid.SelectionMode is SelectionMode.Multiple )
        {
            SelectItemCore();
        }
        else
        {
            selectedItems.Clear();
            SelectItemCore();
        }

        await DataGrid.SelectedItemsChanged.InvokeAsync( selectedItems );
        DataGrid.Refresh();

        void SelectItemCore()
        {
            if( selected )
            {
                selectedItems.Add( item );
            }
            else
            {
                selectedItems.Remove( item );
            }
        }
    }

    private bool AreAllItemsSelected()
    {
        return DataGrid.Data is not null 
            && DataGrid.Data.Count() == DataGrid.SelectedItems.Count;
    }

    private bool IsItemSelected( T item )
    {
        return Context.Owner.SelectedItems.Contains( item );
    }
}
