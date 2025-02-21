// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using LumexUI.Common;
using LumexUI.Utilities;

using Microsoft.JSInterop;

namespace LumexUI.Services;

/// <summary>
/// Provides theme management functionality by interacting with JavaScript.
/// </summary>
public sealed class ThemeService : IAsyncDisposable
{
	private const string JavaScriptPrefix = "theme";

	private readonly Lazy<Task<IJSObjectReference>> _moduleTask;

	/// <summary>
	/// Initializes a new instance of the <see cref="ThemeService"/>.
	/// </summary>
	/// <param name="jsRuntime">The JavaScript runtime to use for invoking JavaScript functions.</param>
	public ThemeService( IJSRuntime jsRuntime )
	{
		_moduleTask = new( () => jsRuntime.InvokeAsync<IJSObjectReference>(
			"import", "./_content/LumexUI/js/utils/theme.js" ).AsTask() );
	}

	/// <summary>
	/// Asynchronously toggles the current theme between light and dark mode.
	/// </summary>
	/// <returns><see langword="true"/> if the dark theme was toggled; otherwise, <see langword="false"/>.</returns>
	public async ValueTask<bool> ToggleAsync()
	{
		var module = await _moduleTask.Value;
		return await module.InvokeAsync<bool>( $"{JavaScriptPrefix}.toggle" );
	}

	internal async ValueTask InitializeAsync( ThemeType type )
	{
		var module = await _moduleTask.Value;
		await module.InvokeVoidAsync( $"{JavaScriptPrefix}.initialize", type.ToDescription() );
	}

	async ValueTask IAsyncDisposable.DisposeAsync()
	{
		if( _moduleTask.IsValueCreated )
		{
			var module = await _moduleTask.Value;
			await module.DisposeAsync();
		}
	}
}
