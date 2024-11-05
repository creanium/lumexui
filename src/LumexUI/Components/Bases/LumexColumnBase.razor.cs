// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using LumexUI.Common;

using Microsoft.AspNetCore.Components;

namespace LumexUI;

/// <summary>
/// Represents a base class for columns in a <see cref="LumexDataGrid{T}"/>.
/// </summary>
/// <typeparam name="T">The type of data represented by each row in the grid.</typeparam>
public abstract partial class LumexColumnBase<T> : LumexComponentBase
{
    /// <summary>
    /// Gets or sets the content to be rendered in the column header.
    /// </summary>
    [Parameter] public RenderFragment<LumexColumnBase<T>>? TitleContent { get; set; }

    /// <summary>
    /// Gets or sets the title text for the column.
    /// </summary>
    [Parameter] public string? Title { get; set; }

    [CascadingParameter] internal DataGridContext<T> Context { get; set; } = default!;

    internal LumexDataGrid<T> DataGrid => Context.Owner;

    /// <inheritdoc />
    protected override void OnInitialized()
    {
        ContextNullException.ThrowIfNull( Context, nameof( LumexDataGrid<T> ) );
    }
}
