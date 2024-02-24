// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using System.Diagnostics.CodeAnalysis;

namespace LumexUI.Utilities;

internal class ParentComponentNullException : Exception
{
    internal ParentComponentNullException( string? paramName )
		: base( paramName ) { }

    internal ParentComponentNullException( string? message, Exception? innerException )
		: base( message, innerException ) { }

    /// <summary>
    /// Throws a <see cref="ParentComponentNullException"/> if <paramref name="component"/> is null.
    /// </summary>
    /// <param name="component">The parent component type argument to validate as non-null.</param>
    /// <param name="componentName">The name of the parent component.</param>
    internal static void ThrowIfNull( [NotNull] object? component, string? componentName )
	{
		if( component is null )
		{
			Throw( componentName );
		}
	}

	[DoesNotReturn]
	internal static void Throw( string? componentName )
	{
		string message = $"Seems you forgot to wrap component within <{componentName} />.";
		throw new ParentComponentNullException( message );
	}
}
