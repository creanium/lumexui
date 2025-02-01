// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using System.Diagnostics.CodeAnalysis;

using LumexUI.Common;
using LumexUI.Utilities;

namespace LumexUI.Styles;

[ExcludeFromCodeCoverage]
internal class Accordion
{
	private readonly static string _fullWidth = ElementClass.Empty()
		.Add( "w-full" )
		.ToString();

	private static ElementClass GetVariantStyles( AccordionVariant variant )
	{
		return ElementClass.Empty()
			.Add( "", when: variant is AccordionVariant.Light )
			.Add( "px-4 shadow-small rounded-medium bg-surface1", when: variant is AccordionVariant.Shadow )
			.Add( "px-4 border border-divider rounded-medium", when: variant is AccordionVariant.Bordered )
			.Add( "group is-splitted flex flex-col gap-2", when: variant is AccordionVariant.Splitted );
	}

	public static string? GetStyles( LumexAccordion accordion )
	{
		var twMerge = accordion.TwMerge;

		return twMerge.Merge(
			ElementClass.Empty()
				.Add( _fullWidth, when: accordion.FullWidth )
				.Add( GetVariantStyles( accordion.Variant ) )
				.Add( accordion.Class )
				.ToString() );
	}
}

[ExcludeFromCodeCoverage]
internal class AccordionItem
{
	private readonly static string _base = ElementClass.Empty()
		.Add( "group-[.is-splitted]:px-4" )
		.Add( "group-[.is-splitted]:bg-surface1" )
		.Add( "group-[.is-splitted]:shadow-small" )
		.Add( "group-[.is-splitted]:rounded-medium" )
		.ToString();

	private readonly static string _trigger = ElementClass.Empty()
		.Add( "flex" )
		.Add( "py-4" )
		.Add( "gap-3" )
		.Add( "w-full" )
		.Add( "items-center" )
		.Add( "outline-none" )
		.ToString();

	private readonly static string _startContent = ElementClass.Empty()
		.Add( "flex-shrink-0" )
		.ToString();

	private readonly static string _titleWrapper = ElementClass.Empty()
		.Add( "flex" )
		.Add( "flex-1" )
		.Add( "flex-col" )
		.Add( "text-start" )
		.ToString();

	private readonly static string _title = ElementClass.Empty()
		.Add( "text-foreground" )
		.Add( "text-large" )
		.ToString();

	private readonly static string _subtitle = ElementClass.Empty()
		.Add( "text-foreground-500" )
		.Add( "text-small" )
		.ToString();

	private readonly static string _indicator = ElementClass.Empty()
		.Add( "text-default-400" )
		.Add( "rotate-0" )
		.Add( "data-[opened]:-rotate-90" )
		.Add( "transition-transform" )
		.ToString();

	private readonly static string _content = ElementClass.Empty()
		.Add( "pb-4" )
		.ToString();

	private readonly static string _disabled = ElementClass.Empty()
		.Add( "opacity-disabled" )
		.Add( "pointer-events-none" )
		.ToString();

	public static AccordionItemSlots GetSlots( LumexAccordionItem accordionItem )
	{
		var twMerge = accordionItem.TwMerge;
		var accordion = accordionItem.Context.Owner;

		return new AccordionItemSlots
		{
			Root = twMerge.Merge(
				ElementClass.Default( _base )
					.Add( _disabled, when: accordionItem.GetDisabledState() )
					.Add( accordion.ItemClasses?.Root )
					.Add( accordionItem.Classes?.Root )
					.Add( accordionItem.Class )
					.ToString() ),

			Heading = twMerge.Merge(
				ElementClass.Empty()
					.Add( accordion.ItemClasses?.Heading )
					.Add( accordionItem.Classes?.Heading )
					.ToString() ),

			Trigger = twMerge.Merge(
				ElementClass.Default( _trigger )
					.Add( accordion.ItemClasses?.Trigger )
					.Add( accordionItem.Classes?.Trigger )
					.ToString() ),

			StartContent = twMerge.Merge(
				ElementClass.Default( _startContent )
					.Add( accordion.ItemClasses?.StartContent )
					.Add( accordionItem.Classes?.StartContent )
					.ToString() ),

			TitleWrapper = twMerge.Merge(
				ElementClass.Default( _titleWrapper )
					.Add( accordion.ItemClasses?.TitleWrapper )
					.Add( accordionItem.Classes?.TitleWrapper )
					.ToString() ),

			Title = twMerge.Merge(
				ElementClass.Default( _title )
					.Add( accordion.ItemClasses?.Title )
					.Add( accordionItem.Classes?.Title )
					.ToString() ),

			Subtitle = twMerge.Merge(
				ElementClass.Default( _subtitle )
					.Add( accordion.ItemClasses?.Subtitle )
					.Add( accordionItem.Classes?.Subtitle )
					.ToString() ),

			Indicator = twMerge.Merge(
				ElementClass.Default( _indicator )
					.Add( accordion.ItemClasses?.Indicator )
					.Add( accordionItem.Classes?.Indicator )
					.ToString() ),

			Content = twMerge.Merge(
				ElementClass.Default( _content )
					.Add( accordion.ItemClasses?.Content )
					.Add( accordionItem.Classes?.Content )
					.ToString() ),
		};
	}
}
