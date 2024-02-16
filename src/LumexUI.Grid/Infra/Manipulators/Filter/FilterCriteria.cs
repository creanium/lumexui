// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using LumexUI.Grid.Data;

namespace LumexUI.Grid.Infra;

internal readonly record struct FilterCriteria
{
	/// <summary>
	/// The string representation of the model property to operate on. 
	/// </summary>
	internal string ModelProperty { get; init; }

	/// <summary>
	/// The filter string to operate with.
	/// </summary>
	internal string FilterString { get; init; }

	/// <summary>
	/// The operation to perform used to create the criteria predicate.
	/// </summary>
	internal FilterOperator FilterOperator { get; init; }
}