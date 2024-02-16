<p align="center">
  <picture>
    <source media="(prefers-color-scheme: dark)" srcset=".github/logo_light.svg">
    <source media="(prefers-color-scheme: light)" srcset=".github/logo_dark.svg">
    <img alt="LumexUI" src=".github/logo_dark.svg" width="350" height="70" style="max-width: 100%;">
  </picture>
</p>

<p align="center">
  A flexible and scalable library of Blazor components designed to streamline the web development experience. 
</p>

## Description

LumexUI is a comprehensive library of components designed for building highly customizable user interfaces.
It offers developers a convenient and intuitive way to style the components using modern CSS custom properties, and extend their functionality.

> ⭐ If you find LumexUI promising, please consider giving a star on GitHub! Your support helps continue to innovate and deliver exciting features

## Philosophy

At LumexUI, we prioritize the developer experience. That's why our library is crafted with developers in mind, offering clean and intuitive APIs that streamline application development. 
Spend less time wrestling with complex code and more time bringing your ideas to life, thanks to LumexUI's developer-friendly approach.

## Quickstart

### Install LumexUI

Open your terminal or package manager and enter the following command.
```
dotnet add package LumexUI
```
Alternatively, you can search for <strong>LumexUI</strong> in the NuGet Package Manager and install it from there.

### Configure your startup file

Inject the vital LumexUI services in your `Startup.cs` to enable seamless integration.
```csharp
using LumexUI.Extensions;
// ...
builder.Services.AddLumexService();
```

### Include required CSS and JS

Add the mandatory files into the `<head>` and the `<body>` respectively to ensure correct functionality.

```html
<head>
  // ...
  <link href="_content/LumexUI/LumexUI.min.css" rel="stylesheet" />
</head>
```
```html
<body>
  // ...
  <script src="_content/LumexUI/LumexUI.min.js"></script>
</body>
```

### Import library's namespace

Import the LumexUI namespace in your `_Imports.razor` file to easily access components throughout your app.

```csharp
@using LumexUI
```

### Add the theme provider

Integrate the `LumexThemeProvider` component into your `MainLayout.razor` file to enable theming.

```razor
<LumexThemeProvider />
```

### Start using LumexUI in your app

Add the simple `LumexButton` component in your code.

```razor
<LumexButton Size="@Size.Medium"
             Color="@ThemeColor.Primary"
             Variant="@ButtonVariant.Smooth"
             OnClick="@HandleClick">
  Click Me
</LumexButton>

@code {
    private void HandleClick()
    {
        // Your click event handler code here
    }
}
```

## Contributing

TODO

## Roadmap

TODO

## License

LumexUI is licensed under the terms of the [MIT licensed](https://github.com/LumexUI/lumex-ui/blob/main/LICENSE).
