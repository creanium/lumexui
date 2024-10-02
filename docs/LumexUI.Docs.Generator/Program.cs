// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using System.Diagnostics;

using LumexUI.Docs.Generator;

internal class Program
{
    private static async Task Main()
    {
        Console.WriteLine( "Docs generation started..." );
        var stopWatch = Stopwatch.StartNew();

        try
        {
            await CodeSnippets.GenerateAsync();
        }
        catch( Exception ex )
        {
            Console.WriteLine( $"An error occurred: {ex.Message}" );
            Environment.Exit( 1 );
        }

        stopWatch.Stop();
        Console.WriteLine( $"Docs generation completed. Time Elapsed: {stopWatch.Elapsed:ss\\.fff} seconds" );
    }
}