// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using LumexUI.Grid.Data;

namespace LumexUI.Grid;

internal class CheckboxCell<TGridItem> : CellBase<TGridItem>
{
	/// <summary>
	/// Indicates whether the <see cref="CheckboxCell{TGridItem}"/> is selected.
	/// </summary>
	internal bool Selected => GetCheckboxColumn().Grid.IsItemSelected( Item );

	/// <summary>
	/// Invoked asynchronously whenever the <see cref="CheckboxCell{TGridItem}"/> is selected.
	/// </summary>
	internal Func<bool, Task> SelectAsync { get; }

	/// <summary>
	/// Constructs an instance of <see cref="CheckboxCell{TGridItem}" />.
	/// </summary>
	/// <param name="column">The column this cell is associated with.</param>
	/// <param name="item">The data item represented by each row in the grid.</param>
	internal CheckboxCell( CheckboxColumn<TGridItem> column, TGridItem item ) : base( column, item )
	{
		SelectAsync = async ( selected ) => await SelectItemAsync( item, selected );
	}

	private async Task SelectItemAsync( TGridItem item, bool selected )
	{
		var column = GetCheckboxColumn();
		var selectedItems = column.Grid.SelectedItems;

		if( column.Grid.SelectionMode == GridSelectionMode.Multiple )
		{
			SelectItemCore( item, selected, ref selectedItems );
		}
		else
		{
			if( selected )
			{
				selectedItems.Clear();
			}

			SelectItemCore( item, selected, ref selectedItems );
		}

		await column.Grid.SelectedItemsChanged.InvokeAsync( selectedItems );
	}

	private void SelectItemCore( TGridItem item, bool selected, ref ICollection<TGridItem> selectedItems )
	{
		if( selected )
		{
			selectedItems.Add( item );
		}
		else
		{
			selectedItems.Remove( item );
		}
	}

	private CheckboxColumn<TGridItem> GetCheckboxColumn() => (CheckboxColumn<TGridItem>)Column;
}
