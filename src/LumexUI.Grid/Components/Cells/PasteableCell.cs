// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using LumexUI.Utilities;

namespace LumexUI.Grid;

internal class PasteableCell<TGridItem, TProp> : EditableCell<TGridItem, TProp>, IPasteableCell
{
	public Func<ValueTask> OnPaste { get; set; }

	public override string Class =>
		new CssBuilder( base.Class )
			.AddClass( Constants.ComponentStates.Invalid, when: IsInvalid && !IsEditing )
		.Build();

	internal bool IsInvalid => GetPasteableColumn().GridPasteContext.IsCellInvalid( Identifier );

	internal PasteableCell( PasteableColumn<TGridItem, TProp> column, TGridItem item ) : base( column, item )
	{
		column.AddCell( this );

		OnClick = async () => await StartEditingItemAsync( item );
		OnPaste = async () => await column.StartPastingAsync( Identifier );
	}

	internal override async Task UpdateValueAsync<TValue>( TValue? value ) where TValue : default
	{
		var column = GetPasteableColumn();

		column.GridPasteContext.TryRemoveInvalidCell( Identifier );

		await base.UpdateValueAsync( value );
		await column.Grid.OnUpdateAsync( column.GridPasteContext.IsGridValid );
	}

	internal void UpdateValueByPaste( string value )
	{
		var column = GetPasteableColumn();

		if( column.IsStringType )
		{
			UpdateValue( value );
		}
		else if( column.IsNumericType )
		{
			TryUpdateNumeric( value );
		}
		else if( column.IsDateTimeType )
		{
			TryUpdateDated( value );
		}
	}

	internal void SetDefaultAndMarkAsInvalid( string value )
	{
		var column = GetPasteableColumn();

		if( column.IsNumericType )
		{
			UpdateValue( default( double ) );
		}
		else if( column.IsDateTimeType )
		{
			UpdateValue( default( DateTime ) );
		}

		GetPasteableColumn().GridPasteContext.MarkCellAsInvalid( Identifier, value );
	}

	private void TryUpdateNumeric( string value )
	{
		bool parsed = double.TryParse( value, out double parsedValue );

		TryUpdateValueCore( parsed, value, parsedValue );
	}

	private void TryUpdateDated( string value )
	{
		bool parsed = DateTime.TryParse( value, out DateTime parsedValue );

		TryUpdateValueCore( parsed, value, parsedValue );
	}

	private void TryUpdateValueCore<TValue>( bool parsed, string value, TValue parsedValue )
	{
		if( parsed || string.IsNullOrWhiteSpace( value ) )
		{
			UpdateValue( parsedValue );

			if( IsInvalid )
			{
				GetPasteableColumn().GridPasteContext.TryRemoveInvalidCell( Identifier );
			}
		}
		else
		{
			SetDefaultAndMarkAsInvalid( value );
		}
	}

	private ValueTask StartEditingItemAsync( TGridItem item )
	{
		var column = GetPasteableColumn();

		if( IsInvalid )
		{
			column.GridPasteContext.TryRemoveInvalidCell( Identifier );
		}

		return column.StartEditingItemAsync( item );
	}

	private PasteableColumn<TGridItem, TProp> GetPasteableColumn() => (PasteableColumn<TGridItem, TProp>)Column;
}
