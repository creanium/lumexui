// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using LumexUI.Grid.Data;
using LumexUI.Grid.Infra;
using LumexUI.Grid.Services;

using Microsoft.AspNetCore.Components;

namespace LumexUI.Grid;

public partial class PasteableColumn<TGridItem, TProp> : EditableColumn<TGridItem, TProp>, IPasteableColumn, IDisposable
{
	[Parameter] public RenderFragment? InvalidTemplate { get; set; }

	[Parameter] public Action<GridPasteEventArgs<TGridItem>>? OnPaste { get; set; }

	[Inject] private IJsApiService JsApiService { get; set; } = default!;

	internal GridPasteContext<TGridItem> GridPasteContext => Grid.GridPasteContext;

	private readonly List<PasteableCell<TGridItem, TProp>> _renderedCells;
	private bool _renderedCellsCleared;

	public PasteableColumn()
	{
		_renderedCells = new();
	}

	internal async ValueTask StartPastingAsync( CellIdentifier<TGridItem> identifier )
	{
		var content = await JsApiService.ReadFromClipboardAsync();

		await GridPasteContext.StartPastingAsync( identifier, content );
	}

	internal void AddCell( PasteableCell<TGridItem, TProp> cell )
	{
		if( _renderedCells.Any( c => c.Identifier == cell.Identifier ) )
		{
			return;
		}

		_renderedCells.Add( cell );
	}

	protected override void OnInitialized()
	{
		Grid.OnColumnRender += ClearRenderedCells;
	}

	protected override void OnAfterRender( bool firstRender )
	{
		_renderedCellsCleared = false;
	}

	protected override CellBase<TGridItem> CreateCell( TGridItem item )
	{
		return CellFactory.Create<TGridItem, TProp>( this, item );
	}

	void IPasteableColumn.Paste( int rowIndex, string[] values )
	{
		for( int i = 0; i < values.Length; i++ )
		{
			if( rowIndex + i < _renderedCells.Count )
			{
				var cell = _renderedCells[rowIndex + i];

				UpdateCellValue( cell, values[i] );
			}

			if( GridEditContext.Editing )
			{
				GridEditContext.StopEditingItem();
			}
		}
	}

	private void UpdateCellValue( PasteableCell<TGridItem, TProp> cell, string value )
	{
		value = value.Trim();

		if( OnPaste is not null )
		{
			var args = new GridPasteEventArgs<TGridItem>( cell.Item, value );

			OnPaste.Invoke( args );

			if( !args.Valid )
			{
				cell.SetDefaultAndMarkAsInvalid( value );
				return;
			}
			else
			{
				value = args.Value;
			}
		}

		cell.UpdateValueByPaste( value );
	}

	private string? RenderInvalidCellValue( PasteableCell<TGridItem, TProp> cell )
	{
		return GridPasteContext.TryGetInvalidCellValue( cell );
	}

	// It is needed in order to clear cached cells of PasteableColums so pasting works correctly.
	// Otherwise, when the data collection is dynamically updated from the client, the cache will be polluted.
	private void ClearRenderedCells()
	{
		if( _renderedCellsCleared )
		{
			return;
		}

		_renderedCells.Clear();
		_renderedCellsCleared = true;
	}

	public void Dispose()
	{
		_renderedCells.Clear();
		Grid.OnColumnRender -= ClearRenderedCells;
	}
}