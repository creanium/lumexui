// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using System.Text;

using Markdig;

namespace LumexUI.Docs.Generator;

internal static class CodeSnippets
{
	private static readonly MarkdownPipeline? _pipeline;

	static CodeSnippets()
	{
		_pipeline = new MarkdownPipelineBuilder()
			.UseAdvancedExtensions()
			.Build();
	}

	internal static void Execute()
	{
		var srcDirPath = GetSrcDirectoryPath();

		if( string.IsNullOrEmpty( srcDirPath ) )
		{
			return;
		}

		GenerateHtmlFiles( srcDirPath );
	}

	private static void GenerateHtmlFiles( string srcDirPath )
	{
		var docsDirPath = Directory.EnumerateDirectories( srcDirPath, @"LumexUI.Docs\LumexUI.Docs.Client" ).First();
		var directoryInfo = new DirectoryInfo( docsDirPath );
		var files = directoryInfo.GetFiles( @"Pages\*.razor", SearchOption.AllDirectories );

		foreach( var file in files )
		{
			if( !file.DirectoryName!.EndsWith( "Examples" ) )
			{
				continue;
			}

			var fileName = file.Name.Replace( ".razor", ".html" );
			var filePath = file.DirectoryName.Replace( "Examples", "Code" );
			var markdownPath = Path.Combine( filePath, fileName );
			var markdownContent = ConvertRazorToMarkdown( file );
			var htmlContent = Markdown.ToHtml( markdownContent, _pipeline );

			if( !Directory.Exists( filePath ) )
			{
				Directory.CreateDirectory( filePath );
			}

			using StreamWriter streamWriter = new( markdownPath );
			streamWriter.Write( htmlContent );
		}
	}

	private static string ConvertRazorToMarkdown( FileInfo file )
	{
		StringBuilder sb = new();

		sb.AppendLine( "```razor" );
		sb.AppendLine( File.ReadAllText( file.FullName ).TrimEnd() );
		sb.AppendLine( "```" );

		return sb.ToString();
	}

	private static string GetSrcDirectoryPath()
	{
		var path = Directory.GetCurrentDirectory();

		while( !string.IsNullOrEmpty( path ) )
		{
			if( Path.GetFileName( path ) == "docs" )
			{
				return path;
			}

			path = Path.GetDirectoryName( path )!;
		}

		return path;
	}
}
