// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using LumexUI.Docs.Generator;
using System.Diagnostics;

internal class Program
{
    private static void Main()
    {
        Console.WriteLine( $"Docs generation started..." );

        var stopWatch = Stopwatch.StartNew();
        CodeSnippets.Execute();
        stopWatch.Stop();

        Console.WriteLine( $"Time Elapsed: {stopWatch.Elapsed:ss\\.fff} seconds" );
    }
}