// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using LumexUI.Utilities;

using Microsoft.AspNetCore.Components.Web;

namespace LumexUI.Grid;

internal class EditableCell<TGridItem, TProp> : PropertyCell<TGridItem, TProp>, IEditableCell
{
	/// <inheritdoc/>
	public Func<ValueTask> OnClick { get; set; }

	/// <inheritdoc/>
	public Func<KeyboardEventArgs, ValueTask> OnKeyUp { get; set; }

	/// <inheritdoc/>
	public override string Class =>
		new CssBuilder( base.Class )
			.AddClass( Constants.ComponentStates.Edit, when: IsEditing )
			.AddClass( Constants.ComponentStates.Dirty, when: IsDirty && !IsEditing )
		.Build();

	internal string? TextValue
	{
		get
		{
			if( GetEditableColumn().IsStringType )
			{
				return (string?)Value.RawValue;
			}

			return null;
		}
	}

	internal double? NumericValue
	{
		get
		{
			if( GetEditableColumn().IsNumericType )
			{
				return Convert.ToDouble( Value.RawValue );
			}

			return null;
		}
	}

	internal bool IsDirty => GetEditableColumn().GridEditContext.IsCellDirty( Identifier );
	internal bool IsEditing => GetEditableColumn().GridEditContext.EditingItemEquals( Item, ColumnIndex );
	internal bool EditingCancelled => GetEditableColumn().GridEditContext.EditingCancelled();

	internal EditableCell( EditableColumn<TGridItem, TProp> column, TGridItem item ) : base( column, item )
	{
		OnClick = async () => await column.StartEditingItemAsync( item );
		OnKeyUp = async ( args ) => await column.HandleKeyUpAsync( args, item );
	}

	internal virtual Task UpdateValueAsync<TValue>( TValue? value )
	{
		if( !EditingCancelled )
		{
			var column = GetEditableColumn();

			if( value is null && column.IsNumericType )
			{
				value = TypeHelper.ConvertFromTo<double, TValue>( default );
			}

			UpdateValue( value );
		}

		return Task.CompletedTask;
	}

	private protected void UpdateValue<TValue>( TValue value )
	{
		var result = TypeHelper.ConvertFromTo<TValue, TProp>( value );

		GetEditableColumn().ValueUpdate.Invoke( Item, result );
	}

	private EditableColumn<TGridItem, TProp> GetEditableColumn() => (EditableColumn<TGridItem, TProp>)Column;
}