// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

namespace LumexUI.Services;

public class DropdownService : IDropdownService
{
	public event Action? OnChange;

	public void CloseAll()
	{
		NotifyStateChanged();
	}

	private void NotifyStateChanged() => OnChange?.Invoke();
}
