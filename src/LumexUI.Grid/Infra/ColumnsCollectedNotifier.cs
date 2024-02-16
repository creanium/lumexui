// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using Microsoft.AspNetCore.Components;

namespace LumexUI.Grid.Infra;

/// <summary>
/// For internal use only. Do not use.
/// </summary>
/// <typeparam name="TGridItem">For internal use only. Do not use.</typeparam>
public class ColumnsCollectedNotifier<TGridItem> : IComponent
{
	private bool _isFirstRender = true;

	[CascadingParameter] internal InternalGridContext<TGridItem> InternalGridContext { get; set; } = default!;

	public void Attach( RenderHandle renderHandle ) { }

	public Task SetParametersAsync( ParameterView parameters )
	{
		if( _isFirstRender )
		{
			_isFirstRender = false;
			parameters.SetParameterProperties( this );
			return InternalGridContext.ColumnsFirstCollected.InvokeCallbacksAsync( null );
		}
		else
		{
			return Task.CompletedTask;
		}
	}
}
