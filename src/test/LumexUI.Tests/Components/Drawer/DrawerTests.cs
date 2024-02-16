// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using Bunit.TestDoubles;

using LumexUI.Services;
using LumexUI.TestComponents.Drawer;

using Microsoft.Extensions.DependencyInjection;

using Moq;

using static Bunit.ComponentParameterFactory;

namespace LumexUI.Tests.Components;

public class DrawerTests : TestContext
{
	public DrawerTests()
	{
		Services.AddScoped<IDrawerService, DrawerService>();
	}

	[Fact]
	public void Drawer_WithHeaderContent_ShowsThisContent()
	{
		// Arrange, Act
		var comp = RenderComponent<DrawerTestComponent>();
		var cut = comp.FindComponent<LumexDrawer>();

		// Assert
		cut.Find( "div.lumex-drawer-header" ).TextContent.Trim().Should().Be( "Header Content" );
	}

	[Fact]
	public void Drawer_WithHeaderContent_ShowsHorizontalDivider()
	{
		// Arrange, Act
		var comp = RenderComponent<DrawerTestComponent>();
		var cut = comp.FindComponent<LumexDrawer>();

		// Assert
		cut.FindComponent<LumexDivider>().Instance.Orientation.Should().Be( Orientation.Horizontal );
	}

	[Fact]
	public void Drawer_WithBodyContent_ShowsThisContent()
	{
		// Arrange, Act
		var comp = RenderComponent<DrawerTestComponent>();
		var cut = comp.FindComponent<LumexDrawer>();

		// Assert
		cut.Find( "div.lumex-drawer-body" ).TextContent.Trim().Should().Be( "Body Content" );
	}

	[Fact]
	public void Drawer_WithHeaderContentAndShowHeader_AlwaysShowsTheHeaderContent()
	{
		// Arrange, Act
		var comp = RenderComponent<DrawerTestComponent>( Parameter( nameof( DrawerTestComponent.ShowHeader ), true ) );
		var cut = comp.FindComponent<LumexDrawer>();

		// Assert
		cut.Find( "div.lumex-drawer-header" ).ClassName.Should().NotContain( "hidden" );
	}

	[Fact]
	public void Drawer_WithHeaderContentAndShowHeader_AlwaysShowsTheHorizontalDivider()
	{
		// Arrange, Act
		var comp = RenderComponent<DrawerTestComponent>( Parameter( nameof( DrawerTestComponent.ShowHeader ), true ) );
		var cut = comp.FindComponent<LumexDrawer>();

		// Assert
		cut.FindComponent<LumexDivider>().Instance.Class.Should().NotContain( "hidden" );
	}

	[Fact]
	public void Drawer_ToggleWhenClosed_GetsOpened()
	{
		// Arrange
		var comp = RenderComponent<DrawerTestComponent>();
		var drawer = comp.Find( ".lumex-drawer" );

		// Act
		comp.Find( "button" ).Click();

		// Assert
		drawer.ClassName.Should().Contain( "lumex-showing" ).And.NotContain( "lumex-shown" );
		drawer.TriggerEvent( "ontransitionend", new EventArgs() );
		drawer.ClassName.Should().Contain( "lumex-shown" ).And.NotContain( "lumex-showing" );

		comp.HasComponent<LumexOverlay>().Should().BeTrue();
	}

	[Fact]
	public void Drawer_ToggleWhenOpened_GetsClosed()
	{
		// Arrange
		var comp = RenderComponent<DrawerTestComponent>();
		var drawer = comp.Find( ".lumex-drawer" );

		comp.Find( "button" ).Click();
		drawer.TriggerEvent( "ontransitionend", new EventArgs() );

		// Act
		comp.Find( "button" ).Click();

		// Assert
		drawer.ClassName.Should().Contain( "lumex-hiding" ).And.NotContain( "lumex-shown" );
		drawer.TriggerEvent( "ontransitionend", new EventArgs() );
		drawer.ClassName.Should().NotContainAny( "lumex-hiding", "lumex-shown", "lumex-showing" );

		comp.HasComponent<LumexOverlay>().Should().BeFalse();
	}

	[Fact]
	public void Drawer_NavigateWhenOpened_GetsClosed()
	{
		// Arrange
		var navMan = Services.GetRequiredService<FakeNavigationManager>();
		var comp = RenderComponent<DrawerTestComponent>();
		var drawer = comp.Find( ".lumex-drawer" );

		comp.Find( "button" ).Click();
		drawer.TriggerEvent( "ontransitionend", new EventArgs() );

		// Act
		navMan.NavigateTo( "someWhereElse" );

		// Assert
		drawer.TriggerEvent( "ontransitionend", new EventArgs() );
		drawer.ClassName.Should().NotContainAny( "lumex-hiding", "lumex-shown", "lumex-showing" );

		comp.HasComponent<LumexOverlay>().Should().BeFalse();
	}

	[Fact]
	public void Drawer_OnDispose_UnregistersSelf()
	{
		// Arrange
		var serviceMock = new Mock<IDrawerService>();
		Services.AddScoped( sp => serviceMock.Object );

		RenderComponent<DrawerTestComponent>();

		// Act
		DisposeComponents();

		// Assert
		serviceMock.Verify( s => s.Unregister( It.IsAny<LumexDrawer>() ) );
	}

	[Fact]
	public void Drawer_OnDispose_UnsubscribesEvents()
	{
		// Arrange
		RenderComponent<DrawerTestComponent>();
		var service = Services.GetRequiredService<FakeNavigationManager>();

		// Act
		DisposeComponents();

		// Assert
		service.GetPropertyValue( "LocationChanged" ).Should().BeNull();
	}
}