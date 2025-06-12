// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using System.Diagnostics.CodeAnalysis;

using LumexUI.Common;
using LumexUI.Utilities;

using TailwindMerge;

namespace LumexUI.Styles;

[ExcludeFromCodeCoverage]
internal static class Alert
{
	private static ComponentVariant? _variant;

	public static ComponentVariant Style( TwMerge twMerge )
	{
		var twVariants = new TwVariants( twMerge );

		return _variant ??= twVariants.Create( new VariantConfig
		{
			Slots = new SlotCollection
			{
				[nameof( AlertSlots.Base )] = new ElementClass()
					.Add( "flex" )
					.Add( "flex-grow" )
					.Add( "flex-row" )
					.Add( "w-full" )
					.Add( "items-start" )
					.Add( "py-3" )
					.Add( "px-4" )
					.Add( "gap-x-1" ),
				
				[nameof( AlertSlots.MainWrapper )] = new ElementClass()
					.Add( "h-full" )
					.Add( "flex-grow" )
					.Add( "min-h-10" )
					.Add( "ms-2" )
					.Add( "flex" )
					.Add( "flex-col" )
					.Add( "box-border" )
					.Add( "items-start" )
					.Add( "text-inherit" )
					.Add( "justify-center" ),

				[nameof( AlertSlots.Title )] = new ElementClass()
					.Add( "text-small" )
					.Add( "w-full" )
					.Add( "font-medium" )
					.Add( "block" )
					.Add( "text-inherit" )
					.Add( "leading-5" ),
				
				[nameof( AlertSlots.Description )] = new ElementClass()
					.Add( "pl-[1px]" )
					.Add( "text-small" )
					.Add( "font-normal" )
					.Add( "text-inherit" ),

				[nameof( AlertSlots.CloseButton )] = new ElementClass()
					.Add( "relative" )
					.Add( "text-inherit" )
					.Add( "translate-x-1" )
					.Add( "-translate-y-1" ),
				
				[nameof( AlertSlots.IconWrapper )] = new ElementClass()
					.Add( "flex-none" )
					.Add( "relative" )
					.Add( "w-9" )
					.Add( "h-9" )
					.Add( "rounded-full" )
					.Add( "grid" )
					.Add( "place-items-center" ),
				
				[nameof( AlertSlots.Icon )] = new ElementClass()
					.Add( "fill-current" )
					.Add( "w-6" )
					.Add( "absolute" )
					.Add( "top-1/2" )
					.Add( "left-1/2" )
					.Add( "-translate-x-1/2" )
					.Add( "-translate-y-1/2" )
			},

			Variants = new VariantCollection
			{
				[nameof( LumexAlert.Variant )] = new VariantValueCollection
				{
					[nameof( AlertVariant.Solid )] = new SlotCollection(),
					[nameof( AlertVariant.Outlined )] = new SlotCollection
					{
						[nameof( AlertSlots.Base )] = "border bg-transparent"
					},
					[nameof( AlertVariant.Flat )] = new SlotCollection
					{
						[nameof( AlertSlots.IconWrapper )] = "shadow-small border-1"
					},
					[nameof( AlertVariant.Faded )] = new SlotCollection
					{
						[nameof( AlertSlots.Base )] = "border"
					}
				},

				[nameof( LumexAlert.Radius )] = new VariantValueCollection
				{
					[nameof( Radius.None )] = new SlotCollection
					{
						[nameof( AlertSlots.Base )] = "rounded-none"
					},
					[nameof( Radius.Small )] = new SlotCollection
					{
						[nameof( AlertSlots.Base )] = "rounded-small"
					},
					[nameof( Radius.Medium )] = new SlotCollection
					{
						[nameof( AlertSlots.Base )] = "rounded-medium"
					},
					[nameof( Radius.Large )] = new SlotCollection
					{
						[nameof( AlertSlots.Base )] = "rounded-large"
					},
					[nameof( Radius.Full )] = new SlotCollection
					{
						[nameof( AlertSlots.Base )] = "rounded-full"
					}
				},

				[nameof( LumexAlert.HideIcon )] = new VariantValueCollection
				{
					[bool.TrueString] = new SlotCollection
					{
						[nameof( AlertSlots.IconWrapper )] = "hidden"
					}
				},
				
				[nameof( LumexAlert.HideIconWrapper )] = new VariantValueCollection
				{
					[bool.TrueString] = new SlotCollection
					{
						[nameof( AlertSlots.Base )] = "gap-x-0",
						[nameof( AlertSlots.IconWrapper )] = "!bg-transparent !shadow-none !border-none"
					}
				}
			},

			CompoundVariants =
			[
				// solid & color
				new CompoundVariant
				{
					Conditions = new()
					{
						[nameof( LumexAlert.Variant )] = nameof( AlertVariant.Solid ),
						[nameof( LumexAlert.Color )] = nameof( ThemeColor.Default )
					},
					Classes = new SlotCollection
					{
						[nameof( AlertSlots.Base )] = ColorVariants.Solid[ThemeColor.Default],
						[nameof( AlertSlots.CloseButton )] = "hover:bg-default-100",
						[nameof( AlertSlots.Icon )] = "text-default-foreground",
					}
				},
				new CompoundVariant
				{
					Conditions = new()
					{
						[nameof( LumexAlert.Variant )] = nameof( AlertVariant.Solid ),
						[nameof( LumexAlert.Color )] = nameof( ThemeColor.Primary )
					},
					Classes = new SlotCollection
					{
						[nameof( AlertSlots.Base )] = ColorVariants.Solid[ThemeColor.Primary]
					}
				},
				new CompoundVariant
				{
					Conditions = new()
					{
						[nameof( LumexAlert.Variant )] = nameof( AlertVariant.Solid ),
						[nameof( LumexAlert.Color )] = nameof( ThemeColor.Secondary )
					},
					Classes = new SlotCollection
					{
						[nameof( AlertSlots.Base )] = ColorVariants.Solid[ThemeColor.Secondary]
					}
				},
				new CompoundVariant
				{
					Conditions = new()
					{
						[nameof( LumexAlert.Variant )] = nameof( AlertVariant.Solid ),
						[nameof( LumexAlert.Color )] = nameof( ThemeColor.Success )
					},
					Classes = new SlotCollection
					{
						[nameof( AlertSlots.Base )] = ColorVariants.Solid[ThemeColor.Success]
					}
				},
				new CompoundVariant
				{
					Conditions = new()
					{
						[nameof( LumexAlert.Variant )] = nameof( AlertVariant.Solid ),
						[nameof( LumexAlert.Color )] = nameof( ThemeColor.Warning )
					},
					Classes = new SlotCollection
					{
						[nameof( AlertSlots.Base )] = ColorVariants.Solid[ThemeColor.Warning]
					}
				},
				new CompoundVariant
				{
					Conditions = new()
					{
						[nameof( LumexAlert.Variant )] = nameof( AlertVariant.Solid ),
						[nameof( LumexAlert.Color )] = nameof( ThemeColor.Danger )
					},
					Classes = new SlotCollection
					{
						[nameof( AlertSlots.Base )] = ColorVariants.Solid[ThemeColor.Danger]
					}
				},
				new CompoundVariant
				{
					Conditions = new()
					{
						[nameof( LumexAlert.Variant )] = nameof( AlertVariant.Solid ),
						[nameof( LumexAlert.Color )] = nameof( ThemeColor.Info )
					},
					Classes = new SlotCollection
					{
						[nameof( AlertSlots.Base )] = ColorVariants.Solid[ThemeColor.Info]
					}
				},

				// flat & color
				new CompoundVariant
				{
					Conditions = new()
					{
						[nameof( LumexAlert.Variant )] = nameof( AlertVariant.Flat ),
						[nameof( LumexAlert.Color )] = nameof( ThemeColor.Default )
					},
					Classes = new SlotCollection
					{
						[nameof( AlertSlots.Base )] = new ElementClass()
							.Add( ColorVariants.Flat[ThemeColor.Default] )
							.Add( "bg-default-100 dark:bg-default-50/50" )
							.Add( "text-default-foreground" ),
						[nameof( AlertSlots.Description )] = "text-default-600",
						[nameof( AlertSlots.CloseButton )] = "text-default-400",
						[nameof( AlertSlots.IconWrapper )] = "bg-default-50 dark:bg-default-100 border-default-200",
					}
				},
				new CompoundVariant
				{
					Conditions = new()
					{
						[nameof( LumexAlert.Variant )] = nameof( AlertVariant.Flat ),
						[nameof( LumexAlert.Color )] = nameof( ThemeColor.Primary )
					},
					Classes = new SlotCollection
					{
						[nameof( AlertSlots.Base )] = new ElementClass()
							.Add( ColorVariants.Flat[ThemeColor.Primary] )
							.Add( "bg-primary-50 dark:bg-primary-50/50" ),
						[nameof( AlertSlots.CloseButton )] = "text-primary-500 hover:bg-primary-200",
						[nameof( AlertSlots.IconWrapper )] = "bg-primary-50 dark:bg-primary-100 border-primary-100",
					}
				},
				new CompoundVariant
				{
					Conditions = new()
					{
						[nameof( LumexAlert.Variant )] = nameof( AlertVariant.Flat ),
						[nameof( LumexAlert.Color )] = nameof( ThemeColor.Secondary )
					},
					Classes = new SlotCollection
					{
						[nameof( AlertSlots.Base )] = new ElementClass()
							.Add( ColorVariants.Flat[ThemeColor.Secondary] )
							.Add( "bg-secondary-50 dark:bg-secondary-50/50" ),
						[nameof( AlertSlots.CloseButton )] = "text-secondary-500 hover:bg-secondary-200",
						[nameof( AlertSlots.IconWrapper )] = "bg-secondary-50 dark:bg-secondary-100 border-secondary-100",
					}
				},
				new CompoundVariant
				{
					Conditions = new()
					{
						[nameof( LumexAlert.Variant )] = nameof( AlertVariant.Flat ),
						[nameof( LumexAlert.Color )] = nameof( ThemeColor.Success )
					},
					Classes = new SlotCollection
					{
						[nameof( AlertSlots.Base )] = new ElementClass()
							.Add( ColorVariants.Flat[ThemeColor.Success] )
							.Add( "bg-success-50 dark:bg-success-50/50" ),
						[nameof( AlertSlots.CloseButton )] = "text-success-500 hover:bg-success-200",
						[nameof( AlertSlots.IconWrapper )] = "bg-success-50 dark:bg-success-100 border-success-100",
					}
				},
				new CompoundVariant
				{
					Conditions = new()
					{
						[nameof( LumexAlert.Variant )] = nameof( AlertVariant.Flat ),
						[nameof( LumexAlert.Color )] = nameof( ThemeColor.Warning )
					},
					Classes = new SlotCollection
					{
						[nameof( AlertSlots.Base )] = new ElementClass()
							.Add( ColorVariants.Flat[ThemeColor.Warning] )
							.Add( "bg-warning-50 dark:bg-warning-50/50" ),
						[nameof( AlertSlots.CloseButton )] = "text-warning-500 hover:bg-warning-200",
						[nameof( AlertSlots.IconWrapper )] = "bg-warning-50 dark:bg-warning-100 border-warning-100",
					}
				},
				new CompoundVariant
				{
					Conditions = new()
					{
						[nameof( LumexAlert.Variant )] = nameof( AlertVariant.Flat ),
						[nameof( LumexAlert.Color )] = nameof( ThemeColor.Danger )
					},
					Classes = new SlotCollection
					{
						[nameof( AlertSlots.Base )] = new ElementClass()
							.Add( ColorVariants.Flat[ThemeColor.Danger] )
							.Add( "bg-danger-50 dark:bg-danger-50/50" ),
						[nameof( AlertSlots.CloseButton )] = "text-danger-500 hover:bg-danger-200",
						[nameof( AlertSlots.IconWrapper )] = "bg-danger-50 dark:bg-danger-100 border-danger-100",
					}
				},
				new CompoundVariant
				{
					Conditions = new()
					{
						[nameof( LumexAlert.Variant )] = nameof( AlertVariant.Flat ),
						[nameof( LumexAlert.Color )] = nameof( ThemeColor.Info )
					},
					Classes = new SlotCollection
					{
						[nameof( AlertSlots.Base )] = new ElementClass()
							.Add( ColorVariants.Flat[ThemeColor.Info] )
							.Add( "bg-info-50 dark:bg-info-50/50" ),
						[nameof( AlertSlots.CloseButton )] = "text-info-500 hover:bg-info-200",
						[nameof( AlertSlots.IconWrapper )] = "bg-info-50 dark:bg-info-100 border-info-100",
					}
				},
				
				// faded & color
				new CompoundVariant
				{
					Conditions = new()
					{
						[nameof( LumexAlert.Variant )] = nameof( AlertVariant.Faded ),
						[nameof( LumexAlert.Color )] = nameof( ThemeColor.Default )
					},
					Classes = new SlotCollection
					{
						[nameof( AlertSlots.Base )] = new ElementClass()
							.Add( "border-default-300 dark:border-default-200" )
							.Add( "bg-default-100 dark:bg-default-50/50"),
						[nameof( AlertSlots.Description )] = "text-default-600",
						[nameof( AlertSlots.CloseButton )] = "text-default-400",
						[nameof( AlertSlots.IconWrapper )] = "bg-default-50 dark:bg-default-100 border-default-200",
					}
				},
				new CompoundVariant
				{
					Conditions = new()
					{
						[nameof( LumexAlert.Variant )] = nameof( AlertVariant.Faded ),
						[nameof( LumexAlert.Color )] = nameof( ThemeColor.Primary )
					},
					Classes = new SlotCollection
					{
						[nameof( AlertSlots.Base )] = new ElementClass()
							.Add( "border-primary-200 dark:border-primary-100" )
							.Add( "bg-primary-50 dark:bg-primary-50/50" ),
						[nameof( AlertSlots.CloseButton )] = "text-primary-500 hover:bg-primary-200",
						[nameof( AlertSlots.IconWrapper )] = "bg-primary-50 dark:bg-primary-100 border-primary-100",
					}
				},
				new CompoundVariant
				{
					Conditions = new()
					{
						[nameof( LumexAlert.Variant )] = nameof( AlertVariant.Faded ),
						[nameof( LumexAlert.Color )] = nameof( ThemeColor.Secondary )
					},
					Classes = new SlotCollection
					{
						[nameof( AlertSlots.Base )] = new ElementClass()
							.Add( "border-secondary-200" )
							.Add( "bg-secondary-50 dark:bg-secondary-50/50" ),
						[nameof( AlertSlots.CloseButton )] = "text-secondary-500 hover:bg-secondary-200",
						[nameof( AlertSlots.IconWrapper )] = "bg-secondary-50 dark:bg-secondary-100 border-secondary-100",
					}
				},
				new CompoundVariant
				{
					Conditions = new()
					{
						[nameof( LumexAlert.Variant )] = nameof( AlertVariant.Faded ),
						[nameof( LumexAlert.Color )] = nameof( ThemeColor.Success )
					},
					Classes = new SlotCollection
					{
						[nameof( AlertSlots.Base )] = new ElementClass()
							.Add( "border-success-300 dark:border-success-100" )
							.Add( "bg-success-50 dark:bg-success-50/50" ),
						[nameof( AlertSlots.CloseButton )] = "text-success-500 hover:bg-success-200",
						[nameof( AlertSlots.IconWrapper )] = "bg-success-50 dark:bg-success-100 border-success-100",
					}
				},
				new CompoundVariant
				{
					Conditions = new()
					{
						[nameof( LumexAlert.Variant )] = nameof( AlertVariant.Faded ),
						[nameof( LumexAlert.Color )] = nameof( ThemeColor.Warning )
					},
					Classes = new SlotCollection
					{
						[nameof( AlertSlots.Base )] = new ElementClass()
							.Add( "border-warning-300 dark:border-warning-100" )
							.Add( "bg-warning-50 dark:bg-warning-50/50" ),
						[nameof( AlertSlots.CloseButton )] = "text-warning-500 hover:bg-warning-200",
						[nameof( AlertSlots.IconWrapper )] = "bg-warning-50 dark:bg-warning-100 border-warning-100",
					}
				},
				new CompoundVariant
				{
					Conditions = new()
					{
						[nameof( LumexAlert.Variant )] = nameof( AlertVariant.Faded ),
						[nameof( LumexAlert.Color )] = nameof( ThemeColor.Danger )
					},
					Classes = new SlotCollection
					{
						[nameof( AlertSlots.Base )] = new ElementClass()
							.Add( "border-danger-200 dark:border-danger-100" )
							.Add( "bg-danger-50 dark:bg-danger-50/50" ),
						[nameof( AlertSlots.CloseButton )] = "text-danger-500 hover:bg-danger-200",
						[nameof( AlertSlots.IconWrapper )] = "bg-danger-50 dark:bg-danger-100 border-danger-100",
					}
				},
				new CompoundVariant
				{
					Conditions = new()
					{
						[nameof( LumexAlert.Variant )] = nameof( AlertVariant.Faded ),
						[nameof( LumexAlert.Color )] = nameof( ThemeColor.Info )
					},
					Classes = new SlotCollection
					{
						[nameof( AlertSlots.Base )] = new ElementClass()
							.Add( "border-info-200 dark:border-info-100" )
							.Add( "bg-info-50 dark:bg-info-50/50" ),
						[nameof( AlertSlots.CloseButton )] = "text-info-500 hover:bg-info-200",
						[nameof( AlertSlots.IconWrapper )] = "bg-info-50 dark:bg-info-100 border-info-100",
					}
				}
			]
		} );
	}
}