// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using LumexUI.Common;
using LumexUI.Services;
using LumexUI.Utilities;

using Microsoft.AspNetCore.Components;

namespace LumexUI;

public partial class LumexDropdown : IDisposable
{
	[CascadingParameter] public LumexDropdownItem? Parent { get; set; }

	[Parameter] public RenderFragment? ChildContent { get; set; }

	[Parameter] public string? Label { get; set; }

	[Parameter] public ThemeColor Color { get; set; } = ThemeColor.Primary;

	[Parameter] public Size Size { get; set; } = Size.Medium;

	[Parameter] public Position IconPosition { get; set; } = Position.End;

	[Parameter] public ButtonVariant Variant { get; set; } = ButtonVariant.Solid;

	[Inject] private IDropdownService DropdownService { get; set; } = default!;

	protected override string RootClass =>
		new CssBuilder( "lumex-dropdown" )
			.AddClass( base.RootClass )
		.Build();

	private bool _opened;

	internal void ToggleDropdown( bool closeAll = true )
	{
		if( _opened )
		{
			CloseAllOrThis( closeAll );
		}
		else
		{
			Open();
		}
	}

	protected override void OnInitialized()
	{
		DropdownService.OnChange += Close;
	}

	private void Open()
	{
		_opened = true;
		StateHasChanged();
	}

	private void Close()
	{
		_opened = false;
		StateHasChanged();
	}

	private void CloseAllOrThis( bool closeAll )
	{
		if( closeAll )
		{
			DropdownService.CloseAll();
		}
		else
		{
			Close();
		}
	}

	public void Dispose()
	{
		DropdownService.OnChange -= Close;
	}
}