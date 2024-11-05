namespace LumexUI;

/// <summary>
/// Parameters for data to be supplied by a <see cref="LumexDataGrid{T}"/>'s <see cref="LumexDataGrid{T}.DataSource"/>.
/// </summary>
/// <typeparam name="T">The type of data represented by each row in the grid.</typeparam>
public readonly struct DataSourceRequest<T>
{
    /// <summary>
    /// Gets or sets the maximum number of items to be supplied.
    /// </summary>
    public int? Count { get; init; }

    /// <summary>
    /// Gets or sets the zero-based index of the first item to be supplied.
    /// </summary>
    public int StartIndex { get; init; }

    /// <summary>
    /// Gets or sets a token that indicates if the request should be cancelled.
    /// </summary>
    public CancellationToken CancellationToken { get; init; }

    internal DataSourceRequest(
        int? count,
        int startIndex, 
        CancellationToken cancellationToken )
    {
        Count = count;
        StartIndex = startIndex;
        CancellationToken = cancellationToken;
    }
}
