// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using Microsoft.AspNetCore.Components;

namespace LumexUI.Grid;

public partial class ExpandColumn<TGridItem>
{
	[CascadingParameter] public string? ColumnsCollectionId { get; set; }

	private string _openIcon = Icons.Rounded.ExpandMore;
	private string _closeIcon = Icons.Rounded.ExpandLess;

	public ExpandColumn()
	{
		Width = "64px";
	}

	protected override CellBase<TGridItem> CreateCell( TGridItem item )
	{
		return CellFactory.Create( this, item );
	}
}