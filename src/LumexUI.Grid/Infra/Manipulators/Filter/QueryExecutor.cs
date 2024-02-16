// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using System.Linq.Expressions;
using System.Reflection;

using LumexUI.Grid.Data;

namespace LumexUI.Grid.Infra;

internal static class QueryExecutor<TGridItem>
{
	private static readonly MethodInfo _whereMethod = typeof( Queryable )
		.GetMethods()
		.First( m => m.Name == nameof( Queryable.Where ) && m.GetParameters().Length == 2 )
		.MakeGenericMethod( typeof( TGridItem ) );

	internal static IQueryable<TGridItem> Execute( Query query, IEnumerable<TGridItem> collection )
	{
		var parameterExp = Expression.Parameter( typeof( TGridItem ), "p" );
		var body = GenerateWhereExpression( query, parameterExp );

		if( body is null )
		{
			return default!;
		}

		var queryable = collection.AsQueryable();

		var whereMethodCall = Expression.Call(
				_whereMethod,
				queryable.Expression,
				Expression.Lambda<Func<TGridItem, bool>>( body, parameterExp ) );

		return queryable.Provider.CreateQuery<TGridItem>( whereMethodCall );
	}

	private static Expression? GenerateWhereExpression( Query query, Expression parameterExp )
	{
		if( query.FilterCriteria.Count <= 0 )
		{
			return default!;
		}

		Expression? whereExp = null;

		foreach( var criteria in query.FilterCriteria )
		{
			var propertyExp = criteria.ModelProperty.Split( '.' ).Aggregate( parameterExp, Expression.Property );
			var tempExp = GenerateExpressionCall( criteria, propertyExp );

			if( tempExp is null )
			{
				continue;
			}

			tempExp = Expression.AndAlso(
				Expression.NotEqual( propertyExp, Expression.Constant( null ) ),
				tempExp );

			whereExp = whereExp is null
				? tempExp
				: Expression.OrElse( whereExp, tempExp );
		}

		return whereExp;
	}

	private static Expression? GenerateExpressionCall( FilterCriteria criteria, Expression? propertyExp )
	{
		switch( criteria.FilterOperator )
		{
			case FilterOperator.Contains:
				var method = typeof( string ).GetMethod( "Contains", new[] { typeof( string ), typeof( StringComparison ) } )!;
				var comparisonValue = Expression.Constant( criteria.FilterString );
				var comparisonType = Expression.Constant( StringComparison.OrdinalIgnoreCase );

				return Expression.Call( propertyExp, method, comparisonValue, comparisonType );
			case FilterOperator.None:
				break;
			case FilterOperator.And:
				break;
			case FilterOperator.Or:
				break;
			case FilterOperator.NotContains:
				break;
			case FilterOperator.Equal:
				break;
			case FilterOperator.NotEqual:
				break;
			case FilterOperator.GreaterThan:
				break;
			case FilterOperator.GreaterThanEqual:
				break;
			case FilterOperator.LessThan:
				break;
			case FilterOperator.LessThanEqual:
				break;
			default:
				break;
		}

		return default;
	}
}
