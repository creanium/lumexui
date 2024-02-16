// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using LumexUI.Grid.Data;

namespace LumexUI.Grid.Infra;

internal sealed class GridEditContext<TGridItem>
{
	private readonly LumexGrid<TGridItem> _dataGrid;

	private bool _editingCancelled;
	private TGridItem? _editingItem;
	private ColumnBase<TGridItem>? _editingColumn;
	private List<ColumnBase<TGridItem>>? _editableColumns;
	private List<TGridItem>? _dataGridData;

	internal ICollection<CellIdentifier<TGridItem>> DirtyCells { get; }

	internal bool Editing => _editingItem is not null && _editingColumn is not null;

	internal GridEditContext( LumexGrid<TGridItem> dataGrid )
	{
		_dataGrid = dataGrid;

		DirtyCells = new HashSet<CellIdentifier<TGridItem>>();
	}

	internal bool IsCellDirty( CellIdentifier<TGridItem> identifier ) => DirtyCells.Contains( identifier );

	internal void MarkCellAsDirty( CellIdentifier<TGridItem> identifier )
	{
		if( !DirtyCells.Contains( identifier ) )
		{
			DirtyCells.Add( identifier );
		}
	}

	internal ValueTask StartEditingItemAsync( ColumnBase<TGridItem> column, TGridItem item )
	{
		_dataGridData ??= _dataGrid.Data?.ToList();
		_editableColumns ??= _dataGrid.RenderedColumns.Where( c => c is IEditableColumn ).ToList();

		_editingItem = item;
		_editingColumn = column;
		_editingCancelled = false;

		return _dataGrid.OnEditAsync( item, column );
	}

	internal ValueTask StartEditingNextCellAsync( ColumnBase<TGridItem> column )
	{
		return StartEditingCellCoreAsync( column, factor: 1 );
	}

	internal ValueTask StartEditingPrevCellAsync( ColumnBase<TGridItem> column )
	{
		return StartEditingCellCoreAsync( column, factor: -1 );
	}

	internal ValueTask StartEditingNextRowCellAsync()
	{
		if( _editingItem is null ||
			_dataGridData is null ||
			_editingColumn is null )
		{
			return ValueTask.CompletedTask;
		}

		var rowIndex = _dataGridData.IndexOf( _editingItem );
		var nextRow = _dataGridData.ElementAtOrDefault( rowIndex + 1 );

		if( nextRow is null )
		{
			StopEditingItem();
			return ValueTask.CompletedTask;
		}

		return StartEditingItemAsync( _editingColumn, nextRow );
	}

	internal void StopEditingItem()
	{
		_editingItem = default;
		_editingColumn = default;
	}

	internal void CancelEditingItem()
	{
		_editingCancelled = true;
		StopEditingItem();
	}

	internal bool EditingCancelled() => _editingCancelled;

	internal bool EditingItemEquals( TGridItem item, int columnId )
	{
		if( !Editing ||
			_editingColumn is null ||
			_editingColumn.Index != columnId )
		{
			return false;
		}

		return _editingItem!.Equals( item );
	}

	private ValueTask StartEditingCellCoreAsync( ColumnBase<TGridItem> column, int factor )
	{
		if( _editingItem is null ||
			_dataGridData is null ||
			_editableColumns is null )
		{
			return ValueTask.CompletedTask;
		}

		var columnIndex = _editableColumns.IndexOf( column );
		var nextColumn = _editableColumns.ElementAtOrDefault( columnIndex + factor );

		if( nextColumn is null )
		{
			nextColumn = factor == -1 ? _editableColumns[^1] : _editableColumns[0];

			var rowIndex = _dataGridData.IndexOf( _editingItem );
			_editingItem = _dataGridData[rowIndex + factor];
		}

		return StartEditingItemAsync( nextColumn, _editingItem );
	}
}
