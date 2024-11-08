using System.Linq.Expressions;

using Microsoft.AspNetCore.Components;

namespace LumexUI;

/// <summary>
/// A component representing a <see cref="LumexDataGrid{T}"/> column whose cells display a single value.
/// </summary>
/// <typeparam name="T">The type of data represented by each row in the grid.</typeparam>
/// <typeparam name="P">The type of the value being displayed in the column's cells.</typeparam>
public partial class PropertyColumn<T, P> : LumexColumnBase<T>
{
    /// <summary>
    /// Gets or sets the content to be rendered for each row in the table.
    /// </summary>
    [Parameter] public RenderFragment<T>? Content { get; set; }

    /// <summary>
    /// Gets or sets the value to be displayed in this column's cells.
    /// </summary>
    [Parameter, EditorRequired] public Expression<Func<T, P>> Property { get; set; } = default!;

    /// <summary>
    /// Gets or sets a format string for the value.
    /// </summary>
    /// <remarks>
    /// Requires the <typeparamref name="P"/> type to implement <see cref="IFormattable" />.
    /// </remarks>
    [Parameter] public string? Format { get; set; }

    /// <inheritdoc/>
    public override SortBuilder<T>? SortBy
    {
        get => _sortBuilder;
        set => throw new NotSupportedException( 
            $"PropertyColumn generates this member internally. For custom sorting rules, see '{typeof( TemplateColumn<T> )}'." );
    }

    private Expression<Func<T, P>>? _lastAssignedProperty;
    private Type? _nullableUnderlyingTypeOrNull;
    private Func<T, string?>? _cellTextFunc;
    private SortBuilder<T>? _sortBuilder;

    /// <inheritdoc />
    protected override void OnParametersSet()
    {
        // We have to do a bit of pre-processing on the lambda expression.
        // Only do that if it's new or changed.
        if( _lastAssignedProperty != Property )
        {
            _lastAssignedProperty = Property;
            var compiledPropertyFunc = Property.Compile();

            if( !string.IsNullOrEmpty( Format ) )
            {
                // If the type is nullable, we're interested in formatting the underlying type
                _nullableUnderlyingTypeOrNull = Nullable.GetUnderlyingType( typeof( P ) );
                if( !typeof( IFormattable ).IsAssignableFrom( _nullableUnderlyingTypeOrNull ?? typeof( P ) ) )
                {
                    throw new InvalidOperationException(
                        $"{GetType()} requires the type '{typeof( P )}' to implement '{typeof( IFormattable )}'" +
                        $"when the '{nameof( Format )}' parameter is supplied." );
                }

                _cellTextFunc = FormatCellValue();
            }
            else
            {
                _cellTextFunc = item => compiledPropertyFunc!( item )?.ToString();
            }

            _sortBuilder = SortBuilder<T>.ByAscending( Property );
        }

        if( Title is null && Property.Body is MemberExpression memberExpression )
        {
            Title = memberExpression.Member.Name;
        }
    }

    private Func<T, string> FormatCellValue()
    {
        var toStringMethod = typeof( IFormattable ).GetMethod( "ToString" )!;
        ParameterExpression propVar = Expression.Variable( typeof( P ), "prop" );

        Expression toStringCallTarget = _nullableUnderlyingTypeOrNull == null
            ? propVar
            : Expression.Call(
                propVar,
                typeof( P ).GetMethod( "GetValueOrDefault", Type.EmptyTypes )! );

        MethodCallExpression toStringCall = Expression.Call(
            toStringCallTarget,
            toStringMethod,
            Expression.Property( Expression.Constant( this ), nameof( Format ) ),
            Expression.Constant( null, typeof( IFormatProvider ) ) );

        Expression conditionalCall = default( P ) == null
            ? Expression.Condition(
                Expression.Equal( propVar, Expression.Constant( null, typeof( P ) ) ),
                Expression.Constant( null, typeof( string ) ),
                toStringCall )
            : toStringCall;

        BlockExpression block = Expression.Block( [propVar],
            Expression.Assign( propVar, Property.Body ),
            conditionalCall );

        return Expression.Lambda<Func<T, string>>( block, Property.Parameters[0] ).Compile();
    }
}
