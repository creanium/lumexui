// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using Microsoft.AspNetCore.Components;

namespace LumexUI;

/// <summary>
/// A component representing a data grid, used to display and manage collections of data.
/// </summary>
/// <typeparam name="T">The type of data represented by each row in the grid.</typeparam>
[CascadingTypeParameter( nameof( T ) )]
public partial class LumexDataGrid<T> : LumexComponentBase
{
    /// <summary>
    /// Gets or sets the content to be rendered inside the data grid.
    /// </summary>
    [Parameter] public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Gets or sets a queryable source of data for the grid.
    /// <para>
    /// This could be in-memory data converted to queryable using the
    /// <see cref="Queryable.AsQueryable{IEnumerable}"/> extension method,
    /// or an EntityFramework DataSet or an <see cref="IQueryable"/> derived from it.
    /// </para>
    /// </summary>
    [Parameter] public IQueryable<T>? Data { get; set; }

    private readonly DataGridContext<T> _context;
    private readonly List<LumexColumnBase<T>> _columns;
    private readonly RenderFragment _renderColumnHeaders;
    private readonly RenderFragment _renderNonVirtualizedRows;

    private bool _collectingColumns; // Columns might re-render themselves arbitrarily. We only want to capture them at a defined time.
    private ICollection<T> _currentNonVirtualizedItems;

    /// <summary>
    /// Initializes a new instance of the <see cref="LumexDataGrid{T}"/>.
    /// </summary>
    public LumexDataGrid()
    {
        _columns = [];
        _currentNonVirtualizedItems = [];
        _context = new DataGridContext<T>( this );
        _renderColumnHeaders = RenderColumnHeaders;
        _renderNonVirtualizedRows = RenderNonVirtualizedRows;
    }

    internal void AddColumn( LumexColumnBase<T> column )
    {
        if( _collectingColumns )
        {
            _columns.Add( column );
        }
    }

    private void StartCollectingColumns()
    {
        _columns.Clear();
        _collectingColumns = true;
    }

    private void FinishCollectingColumns()
    {
        _collectingColumns = false;
    }
}
