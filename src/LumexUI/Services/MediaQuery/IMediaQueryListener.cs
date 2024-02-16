// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

namespace LumexUI.Services;

/// <summary>
/// Provides a straightforward way to listen for and respond to changes in media query conditions.
/// </summary>
public interface IMediaQueryListener : IAsyncDisposable
{
	/// <summary>
	/// Gets a value indicating whether the <see href="https://developer.mozilla.org/en-US/docs/Web/API/Document">document</see> 
	/// currently matches the media query.
	/// </summary>
	bool Matched { get; }

	/// <summary>
	/// Matches the <see href="https://developer.mozilla.org/en-US/docs/Web/API/Document">document</see>
	/// with the provided <paramref name="mediaQuery"/> to initiate the process of listening for changes to it.
	/// </summary>
	/// <param name="mediaQuery">A string specifying the media query to match the document with.</param>
	/// <param name="onChange">A callback that is triggered when the media changes based on the provided <paramref name="mediaQuery"/>.</param>
	/// <returns>A task that represents the asynchronous match operation.</returns>
	ValueTask MatchAsync( string mediaQuery, Action onChange );
}