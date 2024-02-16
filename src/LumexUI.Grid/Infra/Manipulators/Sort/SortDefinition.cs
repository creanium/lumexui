// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

namespace LumexUI.Grid.Infra;

internal readonly struct SortDefinition<TGridItem>
{
	internal Func<IQueryable<TGridItem>, bool, IOrderedQueryable<TGridItem>> Expression { get; init; }
}