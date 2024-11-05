// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using LumexUI.Common;
using LumexUI.DataGrid.Core;
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
    /// <para>
    /// You should supply either <see cref="Data"/> or <see cref="DataSource"/>, but not both.
    /// </para>
    /// </summary>
    [Parameter] public IQueryable<T>? Data { get; set; }

    /// <summary>
    /// Gets or sets a callback that supplies data for the grid.
    /// <para>
    /// You should supply either <see cref="Data"/> or <see cref="DataSource"/>, but not both.
    /// </para>
    /// </summary>
    [Parameter] public DataSource<T>? DataSource { get; set; }

    /// <summary>
    /// Gets or sets the selection mode for the data grid, determining how rows can be selected.
    /// </summary>
    /// <remarks>
    /// The default is <see cref="SelectionMode.None"/>.
    /// </remarks>
    [Parameter] public SelectionMode SelectionMode { get; set; }

    /// <summary>
    /// Gets or sets the collection of items currently selected in the data grid.
    /// </summary>
    [Parameter] public ICollection<T> SelectedItems { get; set; } = new HashSet<T>();

    /// <summary>
    /// Gets or sets the callback that is invoked when the selection of items in the data grid changes.
    /// </summary>
    [Parameter] public EventCallback<ICollection<T>> SelectedItemsChanged { get; set; }

    private readonly DataGridContext<T> _context;
    private readonly List<LumexColumnBase<T>> _columns;
    private readonly Memoizer<Task> _refreshDataMemoizer;
    private readonly Memoizer<DataGridSlots> _slotsMemoizer;
    private readonly RenderFragment _renderEmptyContent;
    private readonly RenderFragment _renderLoadingContent;
    private readonly RenderFragment _renderColumnHeaders;
    private readonly RenderFragment _renderNonVirtualizedRows;

    private DataGridSlots _slots = default!;

    private bool _collectingColumns; // Columns might re-render themselves arbitrarily. We only want to capture them at a defined time.
    private ICollection<T> _currentNonVirtualizedItems;
    private CancellationTokenSource? _pendingDataLoadCts;

    /// <summary>
    /// Initializes a new instance of the <see cref="LumexDataGrid{T}"/>.
    /// </summary>
    public LumexDataGrid()
    {
        _context = new DataGridContext<T>( this );
        _refreshDataMemoizer = new Memoizer<Task>();
        _slotsMemoizer = new Memoizer<DataGridSlots>();

        _renderEmptyContent = RenderEmptyContent;
        _renderLoadingContent = RenderLoadingContent;
        _renderColumnHeaders = RenderColumnHeaders;
        _renderNonVirtualizedRows = RenderNonVirtualizedRows;

        _columns = [];
        _currentNonVirtualizedItems = [];

        // As a special case, we don't issue the first data load request until we've collected the initial set of columns
        // This is so we can apply default sort order (or any future per-column options) before loading data
        // We use EventCallbackSubscriber to safely hook this async operation into the synchronous rendering flow
        //var refreshData = _refreshDataMemoizer.Memoize( RefreshDataCoreAsync, [Data, DataSource] );
        var refreshData = () => _refreshDataMemoizer.Memoize( RefreshDataCoreAsync, [Data, DataSource] );
        var columnsFirstCollectedSubscriber = new EventCallbackSubscriber<object?>(
            EventCallback.Factory.Create<object?>( this, refreshData ) );
        columnsFirstCollectedSubscriber.SubscribeOrMove( _context.ColumnsFirstCollected );
    }

    /// <summary>
    /// Instructs the grid to re-fetch and render the current data from the supplied data source.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public async Task RefreshDataAsync()
    {
        await RefreshDataCoreAsync();
        StateHasChanged();
    }

    /// <inheritdoc />
    protected override Task OnParametersSetAsync()
    {
        if( Data is not null && DataSource is not null )
        {
            throw new InvalidOperationException(
                $"{GetType()} requires one of {nameof( Data )} or {nameof( DataSource )}, but both were specified." );
        }

        // Perform a re-building only if the dependencies have changed
        _slots = _slotsMemoizer.Memoize( GetSlots, [Class] );

        // Perform a re-query only if the dependencies have changed
        //
        // We don't want to trigger the first data load until we've collected the initial set of columns,
        // because they might perform some action like setting the default sort order, so it would be wasteful
        // to have to re-query immediately.
        return ( _columns.Count > 0 ) ? _refreshDataMemoizer.Memoize( RefreshDataCoreAsync, [Data, DataSource] ) : Task.CompletedTask;
    }

    internal void Refresh() => StateHasChanged();

    internal void AddColumn( LumexColumnBase<T> column )
    {
        if( !_collectingColumns )
        {
            return;
        }

        _columns.Add( column );
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

    // Same as RefreshDataAsync, except without forcing a re-render. We use this from OnParametersSetAsync
    // because in that case there's going to be a re-render anyway.
    private async Task RefreshDataCoreAsync()
    {
        // Move into a "loading" state, cancelling any earlier-but-still-pending load
        _pendingDataLoadCts?.Cancel();
        var thisLoadCts = _pendingDataLoadCts = new CancellationTokenSource();

        var request = new DataSourceRequest<T>( count: null, startIndex: 0, thisLoadCts.Token );
        var result = await ResolveItemsRequestAsync( request );
        if( !thisLoadCts.IsCancellationRequested )
        {
            _currentNonVirtualizedItems = result.Items;
            _pendingDataLoadCts = null;
        }
    }

    private async ValueTask<DataSourceResult<T>> ResolveItemsRequestAsync( DataSourceRequest<T> request )
    {
        if( DataSource is not null )
        {
            return await DataSource( request );
        }
        else if( Data is not null )
        {
            var totalItemCount = Data.Count();
            var result = Data.Skip( request.StartIndex );

            if( request.Count.HasValue )
            {
                result = result.Take( request.Count.Value );
            }

            var resultArray = result.ToArray();
            return DataSourceResult.From( resultArray, totalItemCount );
        }
        else
        {
            return DataSourceResult.From( Array.Empty<T>(), 0 );
        }
    }

    private DataGridSlots GetSlots()
    {
        var slots = Styles.DataGrid.GetStyles( this );

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
