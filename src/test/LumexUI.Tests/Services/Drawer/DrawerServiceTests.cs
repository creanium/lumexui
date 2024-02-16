// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using LumexUI.Services;

using Moq;

namespace LumexUI.Tests.Services;

public class DrawerServiceTests : TestContext
{
	[Fact]
	public void Register_AlreadyRegisteredDrawer_ThrowsInvalidOperationEx()
	{
		// Arrange
		var service = new DrawerService();
		var drawer = Mock.Of<LumexDrawer>();

		service.Register( drawer );

		// Act
		var act = () => service.Register( drawer );

		// Assert
		act.Should().Throw<InvalidOperationException>();
	}
}