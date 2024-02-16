// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace LumexUI.Grid;

public partial class LumexGridCell<TGridItem>
{
	[CascadingParameter] public LumexGridRow<TGridItem> Row { get; set; } = default!;

	/// <summary>
	/// Defines a content to be rendered in the <see cref="LumexGridCell{TGridItem}"/>.
	/// </summary>
	[Parameter] public RenderFragment? ChildContent { get; set; }

	/// <summary>
	/// Defines the instance of the <see cref="CellBase{TGridItem}"/> associated with the <see cref="LumexGridCell{TGridItem}"/>.
	/// </summary>
	[Parameter, EditorRequired] public CellBase<TGridItem> Instance { get; set; } = default!;

	private int TabIndex => Instance is IEditableCell ? 0 : -1;
	private bool ClickStopPropagation => Instance is IEditableCell;
	private bool PastePreventDefault => Instance is IPasteableCell;
	private string? Title => Instance is IPropertyCell propCell ? propCell.DisplayValue : null;

	private CellBase<TGridItem>? _cachedInstance;

	protected override void OnParametersSet()
	{
		_cachedInstance ??= Instance;

		if( _cachedInstance.Equals( Instance ) )
		{
			_cachedInstance = Instance;
		}

		if( Instance is ExpandCell<TGridItem> expandCell )
		{
			expandCell.Row = Row;
		}

		if( Instance is IEditableCell cell && _cachedInstance is IEditableCell cachedInstance )
		{
			if( cell.DisplayValue != cachedInstance.DisplayValue )
			{
				Instance.Column.Grid.GridEditContext.MarkCellAsDirty( Instance.Identifier );
			}
		}
	}

	private async Task HandleClickAsync()
	{
		if( Instance is IEditableCell editCell )
		{
			await editCell.OnClick();
		}
	}

	private async Task HandleKeyUpAsync( KeyboardEventArgs args )
	{
		if( Instance is IEditableCell editCell )
		{
			await editCell.OnKeyUp( args );
		}
	}

	private async Task HandlePasteAsync()
	{
		if( Instance is IPasteableCell pasteCell )
		{
			await pasteCell.OnPaste();
		}
	}
}