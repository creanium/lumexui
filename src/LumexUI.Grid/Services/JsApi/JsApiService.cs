// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using Microsoft.JSInterop;

namespace LumexUI.Grid.Services;

internal class JsApiService : IJsApiService
{
	private readonly IJSRuntime _jsRuntime;

	public JsApiService( IJSRuntime jsRuntime )
	{
		_jsRuntime = jsRuntime;
	}

	public ValueTask BlurActiveElementAsync()
	{
		return _jsRuntime.InvokeVoidAsync( "document.activeElement.blur" );
	}

	public ValueTask<string> ReadFromClipboardAsync()
	{
		return _jsRuntime.InvokeAsync<string>( "navigator.clipboard.readText" );
	}
}
