// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using Microsoft.AspNetCore.Components;

namespace LumexUI;

/// <summary>
/// A component representing a data grid, used to display and manage collections of data.
/// </summary>
/// <typeparam name="T">The type of data represented by each row in the grid.</typeparam>
[CascadingTypeParameter( nameof( T ) )]
public partial class LumexDataGrid<T> : LumexComponentBase
{
    private readonly DataGridContext<T> _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="LumexDataGrid{T}"/>.
    /// </summary>
    public LumexDataGrid()
    {
        _context = new DataGridContext<T>( this );
    }
}
