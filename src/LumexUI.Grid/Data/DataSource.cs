// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

namespace LumexUI.Grid.Data;

/// <summary>
/// A callback that provides data for a <see cref="LumexGrid{TGridItem}"/>.
/// </summary>
/// <typeparam name="TGridItem">The type of data represented by each row in the grid.</typeparam>
/// <param name="request">Parameters describing the data being requested.</param>
/// <returns>A <see cref="ValueTask" /> that gives the data to be displayed.</returns>
public delegate ValueTask<DataSourceResult<TGridItem>> DataSource<TGridItem>( DataSourceRequest<TGridItem> request );