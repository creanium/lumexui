// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using LumexUI.Styles;
using LumexUI.Utilities;

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
    /// Gets or sets the content to be rendered inside the data grid when there is no data available.
    /// </summary>
    [Parameter] public RenderFragment? EmptyContent { get; set; }

    /// <summary>
    /// Gets or sets the content to be rendered inside the data grid while data is loading.
    /// </summary>
    [Parameter] public RenderFragment? LoadingContent { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the data grid is loading.
    /// </summary>
    [Parameter] public bool Loading { get; set; }

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
    private readonly RenderFragment _renderEmptyContent;
    private readonly RenderFragment _renderLoadingContent;
    private readonly RenderFragment _renderColumnHeaders;
    private readonly RenderFragment _renderNonVirtualizedRows;
    private readonly Memoizer<DataGridSlots> _slotsMemoizer;

    private DataGridSlots _slots = default!;

    private bool _collectingColumns; // Columns might re-render themselves arbitrarily. We only want to capture them at a defined time.
    private ICollection<T> _currentNonVirtualizedItems;

    /// <summary>
    /// Initializes a new instance of the <see cref="LumexDataGrid{T}"/>.
    /// </summary>
    public LumexDataGrid()
    {
        _context = new DataGridContext<T>( this );
        _slotsMemoizer = new Memoizer<DataGridSlots>();

        _renderEmptyContent = RenderEmptyContent;
        _renderLoadingContent = RenderLoadingContent;
        _renderColumnHeaders = RenderColumnHeaders;
        _renderNonVirtualizedRows = RenderNonVirtualizedRows;

        _columns = [];
        _currentNonVirtualizedItems = [];
    }

    /// <inheritdoc />
    protected override void OnParametersSet()
    {
        _slots = _slotsMemoizer.Memoize( GetSlots, [Class] );

        _currentNonVirtualizedItems = ResolveItems();
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

    private T[] ResolveItems()
    {
        if( Data is not null )
        {
            return [.. Data];
        }
        else
        {
            return [];
        }
    }

    private DataGridSlots GetSlots()
    {
        var slots = DataGrid.GetStyles( this );

        slots.Base = TwMerge.Merge( slots.Base );
        slots.Wrapper = TwMerge.Merge( slots.Wrapper );
        slots.EmptyWrapper = TwMerge.Merge( slots.EmptyWrapper );
        slots.LoadingWrapper = TwMerge.Merge( slots.LoadingWrapper );
        slots.Table = TwMerge.Merge( slots.Table );
        slots.Thead = TwMerge.Merge( slots.Thead );
        slots.Tbody = TwMerge.Merge( slots.Tbody );
        slots.Tfoot = TwMerge.Merge( slots.Tfoot );
        slots.Tr = TwMerge.Merge( slots.Tr );
        slots.Th = TwMerge.Merge( slots.Th );
        slots.Td = TwMerge.Merge( slots.Td );

        return slots;
    }
}
