// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

namespace LumexUI.Services;

/// <summary>
/// Represents a factory responsible for creating instances of the <see cref="IMediaQueryListener"/>.
/// </summary>
public interface IMediaQueryListenerFactory
{
	/// <summary>
	/// Creates instances of objects implementing the <see cref="IMediaQueryListener"/>.
	/// </summary>
	/// <returns>An instance of <see cref="IMediaQueryListener"/>.</returns>
	IMediaQueryListener Create();
}