using LumexUI.Common;

namespace LumexUI.DataGrid.Core;

internal class DataGridState<T>
{
    private readonly LumexDataGrid<T> _owner;

    public SortState Sort { get; }

    public DataGridState( LumexDataGrid<T> owner )
    {
        _owner = owner;

        Sort = new SortState();
    }

    public void UpdateSortState( LumexColumnBase<T> column, SortDirection direction )
    {
        var ascending = direction switch
        {
            SortDirection.Ascending => true,
            SortDirection.Descending => false,
            SortDirection.Auto => Sort.Column != column || !Sort.Ascending,
            _ => throw new NotSupportedException( $"Unknown sort direction {direction}" ),
        };

        Sort.Column = column;
        Sort.Direction = direction;
        Sort.Ascending = ascending;
    }

    internal class SortState
    {
        public LumexColumnBase<T>? Column { get; set; }
        public SortDirection Direction { get; set; }
        public bool Ascending { get; set; }
    }
}
