namespace LumexUI.Docs.Client.Pages;

public partial class Overview
{
    private readonly Feature[] _features =
    [
        new Feature()
        {
            Title = "TailwindCSS Integration",
            Description = "Every component is crafted using TailwindCSS, offering you the full power of this utility-first CSS framework. This ensures that your designs are modern, flexible, and in line with best practices."
        },
        new Feature()
        {
            Title = "Beautiful Design",
            Description = "Our components are designed with attention to detail, providing a clean and professional look out of the box. You can trust that they will enhance the visual appeal of your application."
        },
        new Feature()
        {
            Title = "Customizable",
            Description = "With extensive customization options, you can easily adapt the components to match your branding and design preferences. The flexibility of TailwindCSS is fully leveraged to give you control over every aspect of your UI."
        },
        new Feature()
        {
            Title = "Performance-Optimized",
            Description = "We prioritize performance in all our components. LumexUI is built to ensure that your applications load quickly and run smoothly, even as they scale."
        },
        new Feature()
        {
            Title = "Conflict-Free Styling",
            Description = "Our additional utility library automatically handles TailwindCSS class conflicts. This ensures that your custom styles override defaults as expected, eliminating potential issues and simplifying the development process."
        }
    ];

    private readonly QuickLink[] _quickLinks =
    [
        new QuickLink()
        {
            Icon = Icons.Rounded.InstallDesktop,
            Link = "docs/getting-started/installation",
            Title = "Installation",
            Description = "Add LumexUI to your Blazor project with simple installation steps."
        },
        new QuickLink()
        {
            Icon = Icons.Rounded.Draw,
            Link = "docs/getting-started/usage",
            Title = "Usage",
            Description = "Learn the basics about using LumexUI components in your projects."
        },
        new QuickLink()
        {
            Icon = Icons.Rounded.Joystick,
            Link = "docs/components",
            Title = "Components Library",
            Description = "Browse the full collection of LumexUI components and discover how to use them."
        },
        new QuickLink()
        {
            Icon = Icons.Rounded.DesignServices,
            Link = "docs/customization",
            Title = "Customizing Components",
            Description = "Discover the various customization options available to tailor components to your needs."
        }
    ];

    private record Feature
    {
        public required string Title { get; init; }
        public required string Description { get; init; }
    }

    private record QuickLink
    {
        public required string Icon { get; init; }
        public required string Link { get; init; }
        public required string Title { get; init; }
        public required string Description { get; init; }
    }
}
