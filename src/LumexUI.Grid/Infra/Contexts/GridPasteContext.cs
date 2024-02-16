// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using LumexUI.Grid.Data;

namespace LumexUI.Grid.Infra;

internal sealed class GridPasteContext<TGridItem>
{
	private readonly LumexGrid<TGridItem> _dataGrid;
	private List<TGridItem>? _dataGridData;

	internal IDictionary<CellIdentifier<TGridItem>, string> InvalidCells { get; private set; } = new Dictionary<CellIdentifier<TGridItem>, string>();

	internal bool IsGridValid => !InvalidCells.Any();

	internal GridPasteContext( LumexGrid<TGridItem> dataGrid )
	{
		_dataGrid = dataGrid;
	}

	internal bool IsCellInvalid( CellIdentifier<TGridItem> identifier )
	{
		return InvalidCells.ContainsKey( identifier );
	}

	internal void MarkCellAsInvalid( CellIdentifier<TGridItem> identifier, string value )
	{
		if( !InvalidCells.ContainsKey( identifier ) )
		{
			InvalidCells.Add( identifier, value );
		}
		else
		{
			InvalidCells[identifier] = value;
		}
	}

	internal void TryRemoveInvalidCell( CellIdentifier<TGridItem> identifier )
	{
		if( InvalidCells.ContainsKey( identifier ) )
		{
			InvalidCells.Remove( identifier );
		}
	}

	internal string? TryGetInvalidCellValue( CellBase<TGridItem> cell )
	{
		CellIdentifier<TGridItem> identifier = GetIdentifierByCell( cell );

		if( InvalidCells.TryGetValue( identifier, out var value ) )
		{
			return value;
		}

		return null;
	}

	internal ValueTask StartPastingAsync( CellIdentifier<TGridItem> identifier, string content )
	{
		_dataGridData ??= _dataGrid.Data?.ToList();

		if( _dataGridData is not null )
		{
			int rowIndex = _dataGridData.IndexOf( identifier.Item );

			return PasteAsync( content, rowIndex, identifier.ColumnIndex );
		}

		return ValueTask.CompletedTask;
	}

	private async ValueTask PasteAsync( string pastedContent, int startRowIndex, int columnIndex )
	{
		string[][] values = ProcessPastedContent( pastedContent );

		for( int i = 0; i < values.Length; i++ )
		{
			if( columnIndex + i < _dataGrid.RenderedColumns.Count )
			{
				if( _dataGrid.RenderedColumns[columnIndex + i] is IPasteableColumn column )
				{
					column.Paste( startRowIndex, values[i] );
				}
			}
		}

		await _dataGrid.RefreshDataAsync();
		await _dataGrid.OnUpdateAsync( IsGridValid );
	}

	private string[][] ProcessPastedContent( string content )
	{
		string[][] columns = Array.Empty<string[]>();

		try
		{
			string[][] rows = content.Split( "\n" )
						.Where( row => !string.IsNullOrEmpty( row ) )
						.Select( row => row.Split( "\t" ) )
						.ToArray();

			columns = Enumerable.Range( 0, rows[0].Length )
				.Select( colIndex => rows.Select( row => row[colIndex] ).ToArray() )
				.ToArray();
		}
		catch( Exception ex )
		{
			string message = "Unable to process pasted content!";

			Console.WriteLine( $"{message} {ex.Message}" );

			_dataGrid.OnError.InvokeAsync( new GridErrorEventArgs( ex, message ) );
		}

		return columns;
	}

	private CellIdentifier<TGridItem> GetIdentifierByCell( CellBase<TGridItem> cell )
	{
		var identifier = new CellIdentifier<TGridItem>( cell.Item, cell.ColumnIndex );

		return identifier;
	}
}
