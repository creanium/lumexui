// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using LumexUI.Grid.Data;
using LumexUI.Grid.Infra;
using LumexUI.Utilities;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace LumexUI.Grid;

public partial class LumexGridRow<TGridItem>
{
	[CascadingParameter] internal InternalGridContext<TGridItem> InternalGridContext { get; set; } = default!;

	/// <summary>
	/// Defines a cells of the <see cref="LumexGridRow{TGridItem}"/>.
	/// </summary>
	[Parameter] public RenderFragment? ChildContent { get; set; }

	/// <summary>
	/// Defines the data item represented by this row in the <see cref="LumexGrid{TGridItem}"/>.
	/// </summary>
	[Parameter] public TGridItem Item { get; set; } = default!;

	/// <summary>
	/// Specifies the index of this row.
	/// </summary>
	[Parameter] public int Index { get; set; }

	/// <summary>
	/// Indicates whether this row is expandable.
	/// </summary>
	[Parameter] public bool Expandable { get; set; }

	internal bool Expanded { get; private set; }

	private LumexGrid<TGridItem> Grid => InternalGridContext.Grid;

	private bool Selected => Grid.IsItemSelected( Item );

	private string ClassToRender =>
		new CssBuilder( "lumex-grid-row" )
			.AddClass( "lumex-grid-row--expandable", when: Expandable )
			.AddClass( Constants.ComponentStates.Selected, when: Selected )
			.AddClass( GetCustomCssClass() )
		.Build();

	private string ExpandableRowCssClass =>
		new CssBuilder( "lumex-grid-row-detail" )
			.AddClass( Constants.ComponentStates.Expanded, when: Expanded )
		.Build();

	private string? StyleToRender =>
		new StyleBuilder()
			.AddStyle( "height", $"{Grid.RowHeight}px", when: Grid.RowHeight.HasValue )
			.AddStyle( "height", $"{Grid.ItemSize}px", when: Grid.Virtualized )
		.NullIfEmpty();

	private async Task HandleClickAsync( MouseEventArgs args )
	{
		await Grid.OnRowClick.InvokeAsync( new GridRowClickedEventArgs<TGridItem>( args, Item, Index ) );

		if( Grid.SelectionMode != GridSelectionMode.None )
		{
			await SelectRowAsync( Item );
		}
	}

	internal void ExpandRow()
	{
		if( Expandable )
		{
			Expanded = !Expanded;

			StateHasChanged();
		}
	}

	private async ValueTask SelectRowAsync( TGridItem item )
	{
		if( Grid.SelectionMode == GridSelectionMode.Multiple )
		{
			ToggleSelectionForMultipleRows( item );
		}
		else
		{
			ToggleSelectionForSingleRow( item );
		}

		await Grid.SelectedItemsChanged.InvokeAsync( Grid.SelectedItems );
	}

	private void ToggleSelectionForMultipleRows( TGridItem item )
	{
		if( Grid.SelectedItems.Contains( item ) )
		{
			Grid.SelectedItems.Remove( item );
		}
		else
		{
			Grid.SelectedItems.Add( item );
		}
	}

	private void ToggleSelectionForSingleRow( TGridItem item )
	{
		if( Grid.SelectedItems.Contains( item ) )
		{
			Grid.SelectedItems.Remove( item );
		}
		else
		{
			Grid.SelectedItems.Clear();
			Grid.SelectedItems.Add( item );
		}
	}

	private string? GetCustomCssClass()
	{
		if( Item is null )
		{
			return null;
		}

		string? customClass = null;

		if( Grid.OnRowRender is not null )
		{
			var args = new GridRowRenderArgs<TGridItem>( Item );

			Grid.OnRowRender.Invoke( args );

			customClass = args.Class;
		}

		return customClass;
	}
}