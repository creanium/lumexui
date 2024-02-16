// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

namespace LumexUI.Grid.Data;

/// <summary>
/// Parameters for data to be supplied by a <see cref="LumexGrid{TGridItem}"/>'s <see cref="LumexGrid{TGridItem}.DataSource"/>.
/// </summary>
/// <typeparam name="TGridItem">The type of data represented by each row in the grid.</typeparam>
public readonly struct DataSourceRequest<TGridItem>
{
	/// <summary>
	/// The zero-based index of the first item to be supplied.
	/// </summary>
	public int StartIndex { get; }

	/// <summary>
	/// If set, the maximum number of items to be supplied. If not set, the maximum number is unlimited.
	/// </summary>
	public int? Count { get; }

	/// <summary>
	/// A token that indicates if the request should be cancelled.
	/// </summary>
	public CancellationToken CancellationToken { get; }

	internal DataSourceRequest( int startIndex, int? count, CancellationToken cancellationToken )
	{
		Count = count;
		StartIndex = startIndex;
		CancellationToken = cancellationToken;
	}
}
