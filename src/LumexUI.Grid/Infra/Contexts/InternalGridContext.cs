// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using LumexUI.Utilities;

namespace LumexUI.Grid.Infra;

internal sealed class InternalGridContext<TGridItem>
{
	internal LumexGrid<TGridItem> Grid { get; }

	internal EventCallbackSubscribable<object?> ColumnsFirstCollected { get; }
	internal string ColumnsCollectionId { get; set; }

	internal InternalGridContext( LumexGrid<TGridItem> grid )
	{
		Grid = grid;

		ColumnsFirstCollected = new();
		ColumnsCollectionId = Identifier.New();
	}
}
