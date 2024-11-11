using System.Diagnostics.CodeAnalysis;

using LumexUI.Utilities;

using TailwindMerge;

namespace LumexUI.Styles;

[ExcludeFromCodeCoverage]
internal class DataGrid
{
    private readonly static string _base = ElementClass.Empty()
        .Add( "relative" )
        .Add( "w-full" )
        .Add( "flex" )
        .Add( "flex-col" )
        .Add( "gap-4" )
        .ToString();

    private readonly static string _wrapper = ElementClass.Empty()
        .Add( "relative" )
        .Add( "p-4" )
        .Add( "flex" )
        .Add( "flex-col" )
        .Add( "justify-between" )
        .Add( "gap-4" )
        .Add( "bg-surface1" )
        .Add( "shadow-small" )
        .Add( "overflow-auto" )
        .ToString();

    private readonly static string _emptyWrapper = ElementClass.Empty()
        .Add( "h-40" )
        .Add( "align-middle" )
        .Add( "text-small" )
        .Add( "text-center" )
        .Add( "text-foreground-400" )
        .ToString();

    private readonly static string _loadingWrapper = ElementClass.Empty()
        .Add( "absolute" )
        .Add( "inset-0" )
        .Add( "flex" )
        .Add( "items-center" )
        .Add( "justify-center" )
        .Add( "text-small" )
        .Add( "bg-surface1/70" )
        .Add( "backdrop-blur-[1px]" )
        .ToString();

    private readonly static string _table = ElementClass.Empty()
        .Add( "min-w-full" )
        .ToString();

    private readonly static string _tHead = ElementClass.Empty()
        .Add( "[&>tr]:first:rounded-lg" )
        .ToString();

    private readonly static string _tr = ElementClass.Empty()
        .Add( "group" )
        .Add( "outline-none" )
        // focus
        .Add( Utils.FocusVisible )
        .ToString();

    private readonly static string _th = ElementClass.Empty()
        .Add( "group/th" )
        .Add( "px-3" )
        .Add( "h-10" )
        .Add( "align-middle" )
        .Add( "bg-default-100" )
        .Add( "text-foreground-500" )
        .Add( "text-tiny" )
        .Add( "font-semibold" )
        .Add( "whitespace-nowrap" )
        .Add( "outline-none" )
        .Add( "first:rounded-s-lg" )
        .Add( "last:rounded-e-lg" )
        .Add( "hover:text-foreground-400" )
        .Add( "data-[sortable=true]:cursor-pointer" )
        // focus
        .Add( Utils.FocusVisible )
        .ToString();

    private readonly static string _td = ElementClass.Empty()
        .Add( "relative" )
        .Add( "py-2" )
        .Add( "px-3" )
        .Add( "align-middle" )
        .Add( "text-small" )
        .Add( "outline-none" )
        // focus
        .Add( Utils.FocusVisible )
        .ToString();

    private readonly static string _placeholder = ElementClass.Empty()
        .Add( "before:block" )
        .Add( "before:w-3/4" )
        .Add( "before:h-4" )
        .Add( "before:rounded-md" )
        .Add( "before:bg-default-100" )
        .ToString();

    private readonly static string _sortIcon = ElementClass.Empty()
         .Add( "inline-block" )
         .Add( "ms-2" )
         .Add( "opacity-0" )
         .Add( "-rotate-90" )
         .Add( "transition-[transform,opacity]" )
         .Add( "data-[visible=true]:opacity-100" )
         .Add( "group-hover/th:opacity-100" )
         .Add( "group-aria-[sort=ascending]/th:rotate-90" )
         .ToString();

    private readonly static string _striped = ElementClass.Empty()
        .Add( "group-data-[odd=true]:bg-default-100" )
        .ToString();

    private readonly static string _stickyHeader = ElementClass.Empty()
        .Add( "sticky" )
        .Add( "top-0" )
        .Add( "z-20" )
        .Add( "[&>tr]:first:shadow-small" )
        .ToString();

    private readonly static string _align = ElementClass.Empty()
        .Add( "data-[align=start]:text-start" )
        .Add( "data-[align=center]:text-center" )
        .Add( "data-[align=end]:text-end" )
        .ToString();

    public static DataGridSlots GetStyles<T>( LumexDataGrid<T> dataGrid, TwMerge twMerge )
    {
        return new DataGridSlots()
        {
            Base = twMerge.Merge(
                ElementClass.Empty()
                    .Add( _base )
                    .Add( dataGrid.Class )
                    .ToString() ),

            Wrapper = twMerge.Merge(
                ElementClass.Empty()
                    .Add( _wrapper )
                    .ToString() ),

            EmptyWrapper = twMerge.Merge(
                ElementClass.Empty()
                    .Add( _emptyWrapper )
                    .ToString() ),

            LoadingWrapper = twMerge.Merge(
                ElementClass.Empty()
                    .Add( _loadingWrapper )
                    .ToString() ),

            Table = twMerge.Merge(
                ElementClass.Empty()
                    .Add( _table )
                    .ToString() ),

            Thead = twMerge.Merge(
                ElementClass.Empty()
                    .Add( _tHead )
                    .Add( _stickyHeader, when: dataGrid.StickyHeader )
                    .ToString() ),

            Tr = twMerge.Merge(
                    ElementClass.Empty()
                    .Add( _tr )
                    .Add( GetHoverableStyles( dataGrid.Hoverable, slot: nameof( _tr ) ) )
                    .ToString() ),

            Th = twMerge.Merge(
                ElementClass.Empty()
                    .Add( _th )
                    .Add( _align )
                    .ToString() ),

            Td = twMerge.Merge(
                ElementClass.Empty()
                    .Add( _td )
                    .Add( _align )
                    .Add( _striped, when: dataGrid.Striped )
                    .Add( GetHoverableStyles( dataGrid.Hoverable, slot: nameof( _td ) ) )
                    .ToString() ),

            Placeholder = twMerge.Merge(
                ElementClass.Empty()
                    .Add( _placeholder )
                    .ToString() ),

            SortIcon = twMerge.Merge(
                ElementClass.Empty()
                    .Add( _sortIcon )
                    .ToString() ),
        };
    }

    private static ElementClass GetHoverableStyles( bool hoverable, string slot )
    {
        return hoverable switch
        {
            true => ElementClass.Empty()
                .Add( "cursor-default", when: slot is nameof( _tr ) )
                .Add( ElementClass.Empty()
                    .Add( "group-aria-[selected=false]:group-hover:bg-default-100/70" )
                    .Add( "first:rounded-s-lg last:rounded-e-lg" ), when: slot is nameof( _td ) ),

            _ => ElementClass.Empty()
        };
    }
}
