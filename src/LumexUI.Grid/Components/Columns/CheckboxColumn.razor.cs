// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using Microsoft.AspNetCore.Components;

namespace LumexUI.Grid;

public partial class CheckboxColumn<TGridItem> : ColumnBase<TGridItem>
{
	[CascadingParameter] public string? ColumnsCollectionId { get; set; }

	/// <summary>
	/// Defines a CSS class to be applied to <see cref="LumexCheckbox"/> component.
	/// </summary>
	[Parameter] public string? CheckboxClass { get; set; }

	internal Func<bool, Task> SelectAll { get; }

	private bool AllSelected
	{
		get
		{
			if( Grid.Data is not null && Grid.SelectedItems.Any() )
			{
				return Grid.Data.Count() == Grid.SelectedItems.Count;
			}

			return false;
		}
	}

	public CheckboxColumn()
	{
		Width = "64px";
		SelectAll = async ( selected ) => await SelectAllItemsAsync( selected );
	}

	protected override CellBase<TGridItem> CreateCell( TGridItem item )
	{
		return CellFactory.Create( this, item );
	}

	private async Task SelectAllItemsAsync( bool selected )
	{
		if( selected )
		{
			Grid.SelectAllItems();
		}
		else
		{
			Grid.SelectedItems.Clear();
		}

		await Grid.SelectedItemsChanged.InvokeAsync( Grid.SelectedItems );
	}
}
