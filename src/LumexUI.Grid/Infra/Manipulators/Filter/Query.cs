// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

namespace LumexUI.Grid.Infra;

internal readonly record struct Query
{
	/// <summary>
	/// The where criteria.
	/// </summary>
	internal ICollection<FilterCriteria> FilterCriteria { get; init; }

	public Query()
	{
		FilterCriteria = new List<FilterCriteria>();
	}
}
