// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using System.Linq.Expressions;
using System.Reflection;

namespace LumexUI.Grid.Components;

/// <summary>
/// A column that can bind to a property of model.
/// </summary>
public interface IPropertyColumn
{
	PropertyInfo? PropertyInfo { get; }
}

/// <summary>
/// A column that can bind to a property of model.
/// </summary>
/// <typeparam name="TGridItem">Model item type</typeparam>
/// <typeparam name="TProp">Type of property</typeparam>
internal interface IPropertyColumn<TGridItem, TProp> : IPropertyColumn
{
	Expression<Func<TGridItem, TProp>> Property { get; set; }
}
