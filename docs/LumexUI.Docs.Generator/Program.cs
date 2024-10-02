// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using System.Diagnostics;

using LumexUI.Docs.Generator;

internal class Program
{
    private static void Main()
    {
        var stopWatch = new Stopwatch();
        Console.WriteLine( "Docs generation started..." );

        stopWatch.Start();
        CodeSnippets.Execute();
        stopWatch.Stop();

        Console.WriteLine( $"Docs generation completed. Time Elapsed: {stopWatch.Elapsed:ss\\.fff} seconds" );
    }
}