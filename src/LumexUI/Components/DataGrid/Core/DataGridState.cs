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

    internal class SortState
    {
        public LumexColumnBase<T>? Column { get; private set; }
        public SortDirection Direction { get; private set; }
        public bool Ascending { get; private set; }

        public void Update( LumexColumnBase<T> column, SortDirection direction )
        {
            var ascending = direction switch
            {
                SortDirection.Ascending => true,
                SortDirection.Descending => false,
                SortDirection.Auto => Column != column || !Ascending,
                _ => throw new NotSupportedException( $"Unknown sort direction {direction}" ),
            };

            Column = column;
            Direction = direction;
            Ascending = ascending;
        }
    }
}
