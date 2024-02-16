// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

namespace LumexUI.Grid.Data;

/// <summary>
/// Describes the conditions in which a <see cref="LumexGrid{TGridItem}"/> columns are filtered.
/// </summary>
internal enum FilterOperator
{
	/// <summary>
	/// No specific filter condition.
	/// </summary>
	None,

	/// <summary>
	/// Filter condition with more than one operand.
	/// </summary>
	And,

	/// <summary>
	/// Filter condition with more than one operand.
	/// </summary>
	Or,

	/// <summary>
	/// Filter condition with an operand that contains a specified character.
	/// </summary>
	Contains,

	/// <summary>
	/// Filter condition with an operand not containing a specified character.
	/// </summary>
	NotContains,

	/// <summary>
	/// Filter condition with equal operands.
	/// </summary>
	Equal,

	/// <summary>
	/// Filter condition with the not equal operands.
	/// </summary>
	NotEqual,

	/// <summary>
	/// Filter condition with one operand greater than another.
	/// </summary>
	GreaterThan,

	/// <summary>
	/// Filter condition with one operand greater than or equal to another.
	/// </summary>
	GreaterThanEqual,

	/// <summary>
	/// Filter condition with one operand less than another.
	/// </summary>
	LessThan,

	/// <summary>
	/// Filter condition with one operand less than or equal to another.
	/// </summary>
	LessThanEqual
}
