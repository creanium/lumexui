// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using System.Linq.Expressions;
using System.Reflection;

using LumexUI.Grid.Components;
using LumexUI.Grid.Data;

using Microsoft.AspNetCore.Components;

namespace LumexUI.Grid;

/// <summary>
/// Represents a <see cref="LumexGrid{TGridItem}"/> column whose cells display a single value.
/// </summary>
/// <typeparam name="TGridItem">The type of data represented by each row in the grid.</typeparam>
/// <typeparam name="TProp">The type of the value being displayed in the column's cells.</typeparam>
public partial class Column<TGridItem, TProp> : TemplateColumn<TGridItem>, IPropertyColumn<TGridItem, TProp>
{
	/// <summary>
	/// Defines the value to be displayed in this column's cells.
	/// </summary>
	[Parameter, EditorRequired] public Expression<Func<TGridItem, TProp>> Property { get; set; } = default!;

	/// <summary>
	/// Optionally specifies a format string for the value.
	///
	/// Using this requires the <typeparamref name="TProp"/> type to implement <see cref="IFormattable" />.
	/// </summary>
	[Parameter] public string? Format { get; set; }

	public PropertyInfo? PropertyInfo { get; private set; }

	protected Expression<Func<TGridItem, TProp>>? LastAssignedProperty { get; private set; }

	private static readonly MethodInfo _toStringMethod = typeof( IFormattable ).GetMethod( "ToString" )!;
	private readonly Type? _nullableUnderlyingTypeOrNull;

	private Func<TGridItem, TProp>? _compiledPropertyFunc;
	private Func<TGridItem, string?>? _cellTextFunc;

	public Column()
	{
		_nullableUnderlyingTypeOrNull = Nullable.GetUnderlyingType( typeof( TProp ) );
	}

	internal CellValue GetCellValue( TGridItem item )
	{
		return new CellValue()
		{
			RawValue = _compiledPropertyFunc!( item ),
			FormattedValue = _cellTextFunc!( item )
		};
	}

	protected override void OnParametersSet()
	{
		if( LastAssignedProperty != Property )
		{
			LastAssignedProperty = Property;
			_compiledPropertyFunc = Property.Compile();

			if( !string.IsNullOrEmpty( Format ) )
			{
				// If the type is nullable, we're interested in formatting the underlying type
				if( !typeof( IFormattable ).IsAssignableFrom( _nullableUnderlyingTypeOrNull ?? typeof( TProp ) ) )
				{
					throw new InvalidOperationException( $"A '{nameof( Format )}' parameter was supplied, but the type '{typeof( TProp )}' does not implement '{typeof( IFormattable )}'." );
				}

				_cellTextFunc = FormatCellValue();
			}
			else
			{
				_cellTextFunc = item => _compiledPropertyFunc!( item )?.ToString();
			}
		}

		TrySetDefaultTitle();
		TryAddSortableColumn();
	}

	protected override CellBase<TGridItem> CreateCell( TGridItem item )
	{
		return CellFactory.Create<TGridItem, TProp>( this, item );
	}

	protected override bool IsSortableByDefault()
	{
		return ( Sortable.HasValue && Sortable.Value ) || base.IsSortableByDefault();
	}

	protected override void TryAddSortableColumn()
	{
		base.TryAddSortableColumn();

		if( Sortable.HasValue && Sortable.Value )
		{
			Grid.GridSortContext.AddSortableColumn( this, Property );
		}
	}

	private void TrySetDefaultTitle()
	{
		if( Property.Body is MemberExpression memberExpression )
		{
			PropertyInfo = memberExpression.Member as PropertyInfo;

			Title ??= memberExpression.Member.Name;
		}
	}

	private Func<TGridItem, string> FormatCellValue()
	{
		ParameterExpression propVar = Expression.Variable( typeof( TProp ), "prop" );

		Expression toStringCallTarget = _nullableUnderlyingTypeOrNull == null
			? propVar
			: Expression.Call(
				propVar,
				typeof( TProp ).GetMethod( "GetValueOrDefault", Type.EmptyTypes )! );

		MethodCallExpression toStringCall = Expression.Call(
			toStringCallTarget,
			_toStringMethod,
			Expression.Property( Expression.Constant( this ), nameof( Format ) ),
			Expression.Constant( null, typeof( IFormatProvider ) ) );

		Expression conditionalCall = default( TProp ) == null
			? Expression.Condition(
				Expression.Equal( propVar, Expression.Constant( null, typeof( TProp ) ) ),
				Expression.Constant( null, typeof( string ) ),
				toStringCall )
			: toStringCall;

		BlockExpression block = Expression.Block( new[] { propVar },
			Expression.Assign( propVar, Property.Body ),
			conditionalCall );

		return Expression.Lambda<Func<TGridItem, string>>( block, Property.Parameters[0] ).Compile();
	}
}