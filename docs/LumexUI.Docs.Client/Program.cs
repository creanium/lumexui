// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using LumexUI.Docs.Extensions;

using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace LumexUI.Docs.Client;

internal class Program
{
    private static async Task Main( string[] args )
    {
        WebAssemblyHostBuilder builder = WebAssemblyHostBuilder.CreateDefault( args );
        builder.RootComponents.Add<App>( "#app" );
        builder.RootComponents.Add<HeadOutlet>( "head::after" );

        builder.Services.AddScoped( sp => new HttpClient { BaseAddress = new Uri( builder.HostEnvironment.BaseAddress ) } );
        builder.Services.AddLumexDocsServices();

        await builder.Build().RunAsync();
    }
}