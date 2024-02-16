// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using LumexUI.Grid.Components;
using LumexUI.Grid.Data;
using LumexUI.Grid.Infra;
using LumexUI.Utilities;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.JSInterop;

namespace LumexUI.Grid;

/// <summary>
/// A component that displays a grid.
/// </summary>
/// <typeparam name="TGridItem">The type of data represented by each row in the grid.</typeparam>
[CascadingTypeParameter( nameof( TGridItem ) )]
public partial class LumexGrid<TGridItem> : LumexComponentBase, IAsyncDisposable
{
	/// <summary>
	/// Defines a columns of the <see cref="LumexGrid{TGridItem}"/>. For example, you may define columns by adding
	/// components derived from the <see cref="ColumnBase{TGridItem}"/> base class.
	/// </summary>
	[Parameter] public RenderFragment? Columns { get; set; }

	/// <summary>
	/// Defines an optional toolbar template for the <see cref="LumexGrid{TGridItem}"/>.
	/// If specified, this will be used in cells during loading state.
	/// </summary>
	[Parameter] public RenderFragment? ToolbarTemplate { get; set; }

	/// <summary>
	/// Defines an optional loader template for the <see cref="LumexGrid{TGridItem}"/>.
	/// If specified, this will be used in cells during loading state.
	/// </summary>
	[Parameter] public RenderFragment? LoaderTemplate { get; set; }

	/// <summary>
	/// Defines an optional template for the grid when <see cref="Data"/> or <see cref="DataSource"/> are null. 
	/// If specified, this will be used when there's no data to display.
	/// </summary>
	[Parameter] public RenderFragment? NoRecordsTemplate { get; set; }

	/// <summary>
	/// Defines an optional detail row template for the <see cref="LumexGrid{TGridItem}"/>.
	/// This should be used in conjunction with <see cref="ExpandColumn{TGridItem}"/> to work.
	/// </summary>
	[Parameter] public RenderFragment<TGridItem>? DetailRowContent { get; set; }

	/// <summary>
	/// A queryable source of data for the <see cref="LumexGrid{TGridItem}"/>.
	///
	/// <para>You should supply either <see cref="Data"/> or <see cref="DataSource"/>, but not both.</para>
	/// </summary>
	[Parameter] public IQueryable<TGridItem>? Data { get; set; }

	/// <summary>
	/// A callback that supplies data for the <see cref="LumexGrid{TGridItem}"/>.
	///
	/// <para>You should supply either <see cref="Data"/> or <see cref="DataSource"/>, but not both.</para>
	/// </summary>
	[Parameter] public DataSource<TGridItem>? DataSource { get; set; }

	/// <summary>
	/// Specifies whether the grid will be displayed in a loading state.
	/// </summary>
	/// <remarks>Default is <see cref="GridState.Content"/></remarks>
	[Parameter] public GridState State { get; set; }

	/// <summary>
	/// If true, the grid will be rendered with virtualization. This is normally used in conjunction with
	/// scrolling and causes the grid to fetch and render only the data around the current scroll viewport.
	/// This can greatly improve the performance when scrolling through large data sets.
	///
	/// <para>If you use <see cref="Virtualize{TItem}"/>, you should supply a value for <see cref="ItemSize"/> and must
	/// ensure that every row renders with the same constant height.</para>
	///
	/// <para>Generally it's preferable not to use <see cref="Virtualize{TItem}"/> if the amount of data being rendered
	/// is small or if you are using pagination.</para>
	/// </summary>
	[Parameter] public bool Virtualized { get; set; }

	/// <summary>
	/// This is applicable only when using <see cref="Virtualize{TItem}"/>. It defines an expected height in pixels for
	/// each row, allowing the virtualization mechanism to fetch the correct number of items to match the display
	/// size and to ensure accurate scrolling.
	/// </summary>
	/// <remarks>Default is 64</remarks>
	[Parameter] public float ItemSize { get; set; } = 64;

	/// <summary>
	/// Specifies an optional height of the <see cref="LumexGrid{TGridItem}"/>.
	/// <para>The value should be a string containing a valid CSS unit e.g. `100px`, `10rem`, `50%` etc.</para>
	/// </summary>
	///<remarks>Default is <see langword="null"/></remarks>
	[Parameter] public string? Height { get; set; }

	/// <summary>
	/// Specifies an optional height of the grid rows.
	/// </summary>
	///<remarks>Default is <see langword="null"/></remarks>
	[Parameter] public float? RowHeight { get; set; }

	/// <summary>
	/// Indicates whether the grid is responsive.
	/// <para>Note: Since <see cref="LumexGrid{TGridItem}"/> uses `table-layout: fixed` as the default CSS rule,
	/// you will need to set a width of each column manually in order to make the grid look pretty on the smaller devices.
	/// </para>
	/// </summary>
	///<remarks>Default is <see langword="false"/></remarks>
	[Parameter] public bool Responsive { get; set; }

	/// <summary>
	/// Indicates whether the grid is bordered, meaning borders are present on all sides of the table and cells.
	/// </summary>
	///<remarks>Default is <see langword="false"/></remarks>
	[Parameter] public bool Bordered { get; set; }

	/// <summary>
	/// Indicates whether the grid is borderless, meaning borders won't be present at all.
	/// </summary>
	///<remarks>Default is <see langword="false"/></remarks>
	[Parameter] public bool Borderless { get; set; }

	/// <summary>
	/// Indicates whether the grid rows are striped.
	/// </summary>
	///<remarks>Default is <see langword="false"/></remarks>
	[Parameter] public bool Striped { get; set; }

	/// <summary>
	/// Indicates whether the grid rows are hoverable on mouseover.
	/// </summary>
	///<remarks>Default is <see langword="false"/></remarks>
	[Parameter] public bool Hoverable { get; set; }

	/// <summary>
	/// Indicates whether the grid header is sticky on top.
	/// </summary>
	///<remarks>Default is <see langword="false"/></remarks>
	[Parameter] public bool StickyHeader { get; set; }

	/// <summary>
	/// Indicates whether the grid is navigable in editing mode.
	/// If set to true, the grid is allowed to be controlled via keyboard while editing.
	/// </summary>
	///<remarks>Default is <see langword="false"/></remarks>
	[Parameter] public bool Navigable { get; set; }

	/// <summary>
	/// Indicates whether the data grid is filterable.
	/// If set to true, the <see cref="FilterTextBox"/> will show up in the toolbar panel.
	/// </summary>
	///<remarks>Default is <see langword="false"/></remarks>
	[Parameter] public bool Filterable { get; set; }

	/// <summary>
	/// Specifies the row selection mode for the grid.
	/// 
	/// If <see cref="GridSelectionMode.Single"/>, only one row might be selected at once.
	///	If <see cref="GridSelectionMode.Multiple"/>, any amount of rows might be selected at once.
	/// </summary>
	///<remarks>Default is <see cref="GridSelectionMode.None"/></remarks>
	[Parameter] public GridSelectionMode SelectionMode { get; set; }

	/// <summary>
	/// Defines the collection of the grid's currently selected items.
	/// </summary>
	[Parameter] public ICollection<TGridItem> SelectedItems { get; set; } = new HashSet<TGridItem>();

	/// <summary>
	/// Defines an event callback that is fired whenever the grid item is selected or unselected,
	/// causing a change/update of the <see cref="SelectedItems"/> collection.
	/// </summary>
	[Parameter] public EventCallback<ICollection<TGridItem>> SelectedItemsChanged { get; set; }

	/// <summary>
	/// Defines an event callback that is fired whenever the grid row is clicked.
	/// </summary>
	[Parameter] public EventCallback<GridRowClickedEventArgs<TGridItem>> OnRowClick { get; set; }

	/// <summary>
	/// Defines an event callback that is fired whenever the grid item is entering edit mode.
	/// </summary>
	[Parameter] public EventCallback<GridEditEventArgs<TGridItem>> OnEdit { get; set; }

	/// <summary>
	/// Defines an event callback that is fired whenever the grid data is updated.
	/// </summary>
	[Parameter] public EventCallback<GridUpdateEventArgs> OnUpdate { get; set; }

	/// <summary>
	/// Defines an event callback that is fired whenever the grid is throwing an exception.
	/// </summary>
	[Parameter] public EventCallback<GridErrorEventArgs> OnError { get; set; }

	/// <summary>
	/// Defines a callback that is fired whenever the grid row is rendered. For example, you may define
	/// a custom CSS class or classes for the row based on the data.
	/// </summary>
	[Parameter] public Action<GridRowRenderArgs<TGridItem>>? OnRowRender { get; set; }

	[Inject] private IJSRuntime JS { get; set; } = default!;

	internal IQueryable<TGridItem>? CurrentData { get; private set; }

	internal GridSortContext<TGridItem> GridSortContext { get; }
	internal GridEditContext<TGridItem> GridEditContext { get; }
	internal GridPasteContext<TGridItem> GridPasteContext { get; }
	internal GridFilterContext<TGridItem> GridFilterContext { get; }
	internal List<ColumnBase<TGridItem>> RenderedColumns { get; }

	internal event Action? OnColumnRender;

	protected override string RootClass =>
		new CssBuilder( "lumex-datagrid" )
			.AddClass( base.RootClass )
		.Build();

	private string GridContainerCssClass =>
		new CssBuilder( "lumex-grid-container" )
			.AddClass( "lumex-grid-container--responsive", when: Responsive )
		.Build();

	private string GridContainerCssStyle =>
		new StyleBuilder()
			.AddStyle( "height", Height, when: ( Responsive || !string.IsNullOrEmpty( Height ) ) && State == GridState.Content )
		.Build();

	private string GridCssClass =>
		new CssBuilder( "lumex-grid" )
			.AddClass( "lumex-grid--hover", when: Hoverable )
			.AddClass( "lumex-grid--striped", when: Striped )
			.AddClass( "lumex-grid--bordered", when: Bordered )
			.AddClass( "lumex-grid--borderless", when: Borderless )
			.AddClass( "lumex-grid--sticky-header", when: StickyHeader )
			.AddClass( Constants.ComponentStates.Loading, when: Loading )
		.Build();

	private bool Loading => _pendingDataLoadCts is not null;

	// General
	private readonly InternalGridContext<TGridItem> _internalGridContext;

	// Caches of method->delegate conversions
	private readonly RenderFragment _renderColumnGroups;
	private readonly RenderFragment _renderColumnHeaders;
	private readonly RenderFragment _renderLoaderTemplate;
	private readonly RenderFragment _renderToolbarTemplate;
	private readonly RenderFragment _renderNoRecordsTemplate;
	private readonly RenderFragment _renderNonVirtualizedRows;

	private ElementReference _gridReference;
	private Virtualize<(int, TGridItem)>? _virtualizeComponent;
	private ICollection<TGridItem> _currentNonVirtualizedViewItems = Array.Empty<TGridItem>();

	// Caches related to columns collecting process
	private bool _collectingColumns; // Columns might re-render themselves arbitrarily. We only want to capture them at a defined time.

	// The associated ES6 module, which uses document-level event listeners
	private IJSObjectReference? _jsModule;
	private IJSObjectReference? _jsEventDisposable;

	// Caches of instance states
	private CancellationTokenSource? _pendingDataLoadCts;
	private object? _lastAssignedItemsOrProvider;
	private bool _lastVirtualizeState;
	private bool _isFirstRequest;

	/// <summary>
	/// Constructs an instance of <see cref="LumexGrid{TGridItem}" />.
	/// </summary>
	public LumexGrid()
	{
		_internalGridContext = new( this );

		_renderColumnGroups = RenderColumnGroups;
		_renderColumnHeaders = RenderColumnHeaders;
		_renderLoaderTemplate = RenderLoaderTemplate;
		_renderToolbarTemplate = RenderToolbarTemplate;
		_renderNoRecordsTemplate = RenderNoRecordsTemplate;
		_renderNonVirtualizedRows = RenderNonVirtualizedRows;

		RenderedColumns = new();
		GridSortContext = new();
		GridEditContext = new( this );
		GridPasteContext = new( this );
		GridFilterContext = new( this );

		// As a special case, we don't issue the first data load request until we've collected the initial set of columns
		// This is so we can apply default sort order (or any future per-column options) before loading data
		// We use EventCallbackSubscriber to safely hook this async operation into the synchronous rendering flow
		var columnsFirstCollectedSubscriber = new EventCallbackSubscriber<object?>(
			EventCallback.Factory.Create<object?>( this, RefreshDataCoreAsync ) );
		columnsFirstCollectedSubscriber.SubscribeOrMove( _internalGridContext.ColumnsFirstCollected );
	}

	/// <summary>
	/// Instructs the <see cref="LumexGrid{TGridItem}"/> instance to re-fetch and render the current data from the supplied data source.
	/// </summary>
	/// <returns>A <see cref="Task"/> that represents the completion of the operation.</returns>
	public async Task RefreshDataAsync()
	{
		await RefreshDataCoreAsync();
		StateHasChanged();
	}

	/// <summary>
	/// Sets the <see cref="LumexGrid{TGridItem}"/> instance current sort column to the specified <paramref name="column"/>.
	/// </summary>
	/// <param name="column">The column that defines the new sort order.</param>
	/// <param name="direction">The direction of sorting. If the value is <see cref="SortDirection.Auto"/>, then it will toggle the direction on each call.</param>
	/// <returns>A <see cref="Task"/> representing the completion of the operation.</returns>
	public Task SortByColumnAsync( ColumnBase<TGridItem> column, SortDirection direction = SortDirection.Auto )
	{
		bool ascending = direction switch
		{
			SortDirection.Ascending => true,
			SortDirection.Descending => false,
			SortDirection.Auto => GridSortContext.SortByColumn != column || !GridSortContext.SortByAscending,
			_ => throw new NotSupportedException( $"Unknown sort direction {direction}." ),
		};

		GridSortContext.SetSortByColumn( column );
		GridSortContext.SetSortByAscending( ascending );

		StateHasChanged(); // We want to see the updated sort order in the header, even before the data query is completed
		return RefreshDataAsync();
	}

	internal void AddColumn( ColumnBase<TGridItem> column )
	{
		if( !_collectingColumns || !column.Visible )
		{
			return;
		}

		if( column is ExpandColumn<TGridItem> )
		{
			RenderedColumns.Insert( 0, column );
		}
		else
		{
			RenderedColumns.Add( column );
		}

		if( column is ISortableColumn<TGridItem> sortableColumn )
		{
			if( GridSortContext.SortByColumn is null && sortableColumn.DefaultSort.HasValue )
			{
				GridSortContext.SetSortByColumn( column );
				GridSortContext.SetSortByAscending( sortableColumn.DefaultSort.Value == SortDirection.Ascending );
			}
		}
	}

	internal bool IsItemSelected( TGridItem item )
	{
		return SelectedItems.Contains( item );
	}

	internal void SelectAllItems()
	{
		if( Data is not null )
		{
			SelectedItems = new HashSet<TGridItem>( Data );
		}
	}

	internal async ValueTask OnEditAsync( TGridItem item, ColumnBase<TGridItem> column )
	{
		string? columnName = null;

		if( column is IPropertyColumn propertyColumn )
		{
			columnName = propertyColumn.PropertyInfo?.Name;
		}

		await OnEdit.InvokeAsync( new GridEditEventArgs<TGridItem>
		{
			Item = item,
			ColumnId = column.Index,
			ColumnName = columnName ?? column.Title,
			ColumnTitle = column.Title
		} );

		StateHasChanged(); // It's needed here because we've stopped propagation on editable cell click
	}

	internal async ValueTask OnUpdateAsync( bool valid )
	{
		await OnUpdate.InvokeAsync( new GridUpdateEventArgs( valid ) );
	}

	protected override void OnInitialized()
	{
		_lastVirtualizeState = Virtualized;

		ClearSelectionWhenSingleSelectionMode();
	}

	protected override Task OnParametersSetAsync()
	{
		if( Data is not null && DataSource is not null )
		{
			throw new InvalidOperationException( $"{typeof( LumexGrid<TGridItem> )} requires one of {nameof( Data )} or {nameof( DataSource )}, but both were specified." );
		}

		if( DetailRowContent is not null && Virtualized )
		{
			throw new InvalidOperationException( $"{typeof( Virtualize<(int, TGridItem)> )} requires a fixed and known size of the row items, but {nameof( DetailRowContent )} was provided, which may cause scroll glitches." );
		}

		if( Responsive )
		{
			Height = "40rem";
		}

		CurrentData = Data;

		RefreshOnVirtualizedChange( out bool virtualizeStateChanged );

		// Perform a re-query only if the data source or something else has changed
		object? newItemsOrItemsProvider = Data ?? (object?)DataSource;
		bool dataSourceHasChanged = newItemsOrItemsProvider != _lastAssignedItemsOrProvider;

		if( dataSourceHasChanged )
		{
			_lastAssignedItemsOrProvider = newItemsOrItemsProvider;
		}

		// We don't want to trigger the first data load until we've collected the initial set of columns,
		// because they might perform some action like setting the default sort order, so it would be wasteful
		// to have to re-query immediately
		return ( RenderedColumns.Count > 0 && ( dataSourceHasChanged || virtualizeStateChanged ) ) ? RefreshDataCoreAsync() : Task.CompletedTask;
	}

	protected override async Task OnAfterRenderAsync( bool firstRender )
	{
		if( firstRender )
		{
			_jsModule = await JS.InvokeAsync<IJSObjectReference>( "import", "./_content/LumexUI.Grid/Components/LumexGrid.razor.js" );
			_jsEventDisposable = await _jsModule.InvokeAsync<IJSObjectReference>( "init", _gridReference, Navigable );
		}
	}

	private void StartCollectingColumns()
	{
		_collectingColumns = true;
		_internalGridContext.ColumnsCollectionId = Identifier.New();

		RenderedColumns.Clear();
	}

	private void FinishCollectingColumns()
	{
		_collectingColumns = false;
	}

	private async Task RefreshDataCoreAsync()
	{
		// Move into a "loading" state, cancelling any earlier-but-still-pending load
		_pendingDataLoadCts?.Cancel();
		var currLoadCts = _pendingDataLoadCts = new CancellationTokenSource();

		if( _virtualizeComponent is not null )
		{
			// If we're using Virtualize, we have to go through its RefreshDataAsync API otherwise:
			// (1) It won't know to update its own internal state if the provider output has changed
			// (2) We won't know what slice of data to query for
			await _virtualizeComponent.RefreshDataAsync();
			_pendingDataLoadCts = null;
		}
		else
		{
			// If we're not using Virtualize, we build and execute a request against the items provider directly
			DataSourceRequest<TGridItem> dataSourceRequest = new( 0, null, currLoadCts.Token );
			DataSourceResult<TGridItem> dataSourceResult = await ResolveItemsRequestAsync( dataSourceRequest );

			if( !currLoadCts.IsCancellationRequested )
			{
				_currentNonVirtualizedViewItems = dataSourceResult.Data;
				_pendingDataLoadCts = null;
			}
		}
	}

	private async ValueTask<ItemsProviderResult<(int, TGridItem)>> ProvideVirtualizedData( ItemsProviderRequest request )
	{
		// Debounce the requests. This eliminates a lot of redundant queries at the cost of slight lag after interactions.
		// TODO: Consider making this configurable, or smarter (e.g., doesn't delay on first call in a batch, then the amount
		// of delay increases if you rapidly issue repeated requests, such as when scrolling a long way)
		if( _isFirstRequest )
		{
			_isFirstRequest = false;
		}
		else
		{
			await Task.Delay( 50 );
		}

		if( request.CancellationToken.IsCancellationRequested )
		{
			return default;
		}

		DataSourceRequest<TGridItem> dataSourceRequest = new( request.StartIndex, request.Count, request.CancellationToken );
		DataSourceResult<TGridItem> dataSourceResult = await ResolveItemsRequestAsync( dataSourceRequest );

		return !request.CancellationToken.IsCancellationRequested
			? new ItemsProviderResult<(int, TGridItem)>(
					 items: dataSourceResult.Data.Select( ( x, i ) => ValueTuple.Create( i + request.StartIndex + 2, x ) ),
					 totalItemCount: dataSourceResult.TotalItemCount )
			: default;
	}

	private async ValueTask<DataSourceResult<TGridItem>> ResolveItemsRequestAsync( DataSourceRequest<TGridItem> request )
	{
		if( DataSource is not null )
		{
			return await DataSource( request );
		}
		else if( Data is not null )
		{
			var result = GridSortContext.ApplySorting( Data ).Skip( request.StartIndex );

			if( request.Count.HasValue )
			{
				result = result.Take( request.Count.Value );
			}

			return DataSourceResult.From( result.ToArray(), Data.Count() );
		}
		else
		{
			return DataSourceResult.From( Array.Empty<TGridItem>(), 0 );
		}
	}

	private async Task FilterDataAsync( string filterString )
	{
		CurrentData = await GridFilterContext.FilterDataAsync( filterString );

		if( Virtualized )
		{
			await RefreshDataAsync();
		}
	}

	private void ClearSelectionWhenSingleSelectionMode()
	{
		if( SelectedItems.Count > 0 && SelectionMode == GridSelectionMode.Single )
		{
			SelectedItems.Clear();
		}
	}

	private void RefreshOnVirtualizedChange( out bool virtualizeStateChanged )
	{
		virtualizeStateChanged = Virtualized != _lastVirtualizeState;

		if( virtualizeStateChanged )
		{
			_lastVirtualizeState = Virtualized;
			if( !Virtualized )
			{
				_virtualizeComponent = null;
				StateHasChanged();
			}
		}
	}

	/// <inheritdoc />
	public async ValueTask DisposeAsync()
	{
		try
		{
			if( _jsEventDisposable is not null )
			{
				await _jsEventDisposable.InvokeVoidAsync( "destroy" );
				await _jsEventDisposable.DisposeAsync();
			}

			if( _jsModule is not null )
			{
				await _jsModule.DisposeAsync();
			}
		}
		catch( JSDisconnectedException )
		{
			// The JS side may routinely be gone already if the reason we're disposing is that
			// the client disconnected. This is not an error.
		}
	}
}
