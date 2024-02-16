// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using LumexUI.Services;
using LumexUI.TestComponents.MediaQuery;
using LumexUI.Tests.Mocks;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using Microsoft.JSInterop.Infrastructure;

using Moq;

namespace LumexUI.Tests.Services;

public class MediaQueryListenerTests : TestContext
{
	private readonly Mock<IJSRuntime> _jsRuntimeMock;

	public MediaQueryListenerTests()
	{
		_jsRuntimeMock = new Mock<IJSRuntime>();

		_jsRuntimeMock.Setup( js => js.InvokeAsync<IJSVoidResult>(
			"Lumex.mediaQueryListener.destroy", It.IsAny<object[]>() ) )
			.ReturnsAsync( Mock.Of<IJSVoidResult> )
			.Verifiable( Times.Once );
	}

	[Theory]
	[InlineData( "" )]
	[InlineData( "    " )]
	[InlineData( null )]
	public async Task MatchAsync_IncorrectMediaQuery_ThrowsArgumentNullEx( string? mediaQuery )
	{
		// Arrange
		var listener = InitializeListener();

		// Act
		var act = async () => await listener.MatchAsync( mediaQuery!, () => { } );

		// Assert
		await act.Should().ThrowAsync<ArgumentNullException>();
	}

	[Fact]
	public async Task MatchAsync_IncorrectCallback_ThrowsArgumentNullEx()
	{
		// Arrange
		var listener = InitializeListener();

		// Act
		var act = async () => await listener.MatchAsync( "(min-width: 768px)", null! );

		// Assert
		await act.Should().ThrowAsync<ArgumentNullException>();
	}

	[Fact]
	public async Task MatchAsync_InvokesJSInteropMatchMedia()
	{
		// Arrange
		var listener = InitializeListener();

		// Act
		await listener.MatchAsync( "(min-width: 768px)", () => { } );

		// Assert
		_jsRuntimeMock.Verify( js => js.InvokeAsync<IJSVoidResult>(
			"Lumex.mediaQueryListener.matchMedia", It.IsAny<object[]>() ) );
	}

	[Fact]
	public void OnMediaChanged_BeforeMatchAsyncIsCalled_ThrowsInvalidOperationEx()
	{
		// Arrange
		var listener = InitializeListener();

		// Act
		var act = () => listener.MediaQueryChanged( true );

		// Assert
		act.Should().Throw<InvalidOperationException>();
	}

	[Fact]
	public async Task OnMediaChanged_AfterMatchAsyncIsCalled_UpdatesMatchedProp()
	{
		// Arrange
		var expectedMatches = true;
		var listener = InitializeListener();
		await listener.MatchAsync( "(min-width: 768px)", () => { } );

		// Act
		listener.MediaQueryChanged( expectedMatches );

		// Assert
		listener.Matched.Should().Be( expectedMatches );
	}

	[Fact]
	public async Task OnMediaChanged_AfterMatchAsyncIsCalled_InvokesCallback()
	{
		// Arrange
		var callbackInvoked = false;
		var listener = InitializeListener();
		await listener.MatchAsync( "(min-width: 768px)", () => callbackInvoked = true );

		// Act
		listener.MediaQueryChanged( true );

		// Assert
		callbackInvoked.Should().BeTrue();
	}

	[Fact]
	public async Task DisposeAsync_InvokesJSInteropDestroy()
	{
		// Arrange
		var listener = InitializeListener();

		// Act
		await listener.DisposeAsync();

		// Assert
		_jsRuntimeMock.Verify();
	}

	[Fact]
	public async Task DisposeAsync_CalledMultipleTimes_InvokesJSInteropDestroyOnce()
	{
		// Arrange
		var listener = InitializeListener();

		// Act
		await listener.DisposeAsync();
		await listener.DisposeAsync();

		// Assert
		_jsRuntimeMock.Verify();
	}

	[Fact]
	public async Task DisposeAsync_CalledOnce_DisposesSelfReference()
	{
		// Arrange
		var listener = InitializeListener();

		// Act
		await listener.DisposeAsync();

		// Assert
		listener.GetFieldValue( "_disposed" ).Should().Be( true );
	}

	[Fact]
	public void MediaQuery_UsageTest_ShowsCorrectContent()
	{
		// Arrange
		var service = new MediaQueryListenerMock( this );
		Services.AddScoped<IMediaQueryListener>( sp => service );

		// Act
		var cut = RenderComponent<MediaQueryTestComponent>();

		// Assert
		cut.MarkupMatches( "<p>Unmatched</p>" );

		// Fake media change event
		service.FakeMediaChangeEvent( true );

		// Assert
		cut.MarkupMatches( "<p>Matched</p>" );
	}

	private MediaQueryListener InitializeListener()
	{
		return new MediaQueryListener( _jsRuntimeMock.Object );
	}
}