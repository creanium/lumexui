// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using LumexUI.Common;

namespace LumexUI;

internal class TabsContext( LumexTabs owner ) : IComponentContext<LumexTabs>
{
	private bool _collectingTabs;
	private LumexTab? _selectedTab;

	public LumexTabs Owner { get; } = owner;
	public Dictionary<string, LumexTab> Tabs { get; } = [];

	public void Register( LumexTab tab )
	{
		if( !_collectingTabs )
		{
			return;
		}

		if( tab.Id is not null )
		{
			Tabs.Add( tab.Id, tab );
		}
	}

	public void StartCollectingTabs()
	{
		Tabs.Clear();
		_collectingTabs = true;
	}

	public void StopCollectingTabs()
	{
		_collectingTabs = false;
	}

	public LumexTab? GetSelectedTab() => _selectedTab;

	public Task SetSelectedTabAsync( LumexTab tab )
	{
		_selectedTab = tab;
		Owner.Rerender();
		return Owner.SetSelectedIdAsync( tab.Id );
	}
}
