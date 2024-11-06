using System.Diagnostics.CodeAnalysis;

using LumexUI.Utilities;

namespace LumexUI.Styles;

[ExcludeFromCodeCoverage]
internal class DataGrid
{
    private readonly static string _base = ElementClass.Empty()
        .Add( "relative" )
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
        .Add( Utils.FocusVisible )
        .ToString();

    private readonly static string _th = ElementClass.Empty()
        .Add( "group" )
        .Add( "px-3" )
        .Add( "h-10" )
        .Add( "text-start" )
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
        .Add( Utils.FocusVisible )
        .ToString();

    private readonly static string _td = ElementClass.Empty()
        .Add( "relative" )
        .Add( "py-2" )
        .Add( "px-3" )
        .Add( "text-start" )
        .Add( "align-middle" )
        .Add( "text-small" )
        .Add( "outline-none" )
        .Add( Utils.FocusVisible )
        .ToString();

    public static DataGridSlots GetStyles<T>( LumexDataGrid<T> dataGrid )
    {
        return new DataGridSlots()
        {
            Base = ElementClass.Empty()
                .Add( _base )
                .Add( dataGrid.Class )
                .ToString(),

            Wrapper = ElementClass.Empty()
                .Add( _wrapper )
                .ToString(),

            EmptyWrapper = ElementClass.Empty()
                .Add( _emptyWrapper )
                .ToString(),

            LoadingWrapper = ElementClass.Empty()
                .Add( _loadingWrapper )
                .ToString(),

            Table = ElementClass.Empty()
                .Add( _table )
                .ToString(),

            Thead = ElementClass.Empty()
                .Add( _tHead )
                .ToString(),

            Tr = ElementClass.Empty()
                .Add( _tr )
                .Add( GetHoverableStyles( dataGrid.Hoverable, slot: nameof( _tr ) ) )
                .ToString(),

            Th = ElementClass.Empty()
                .Add( _th )
                .ToString(),

            Td = ElementClass.Empty()
                .Add( _td )
                .Add( GetHoverableStyles( dataGrid.Hoverable, slot: nameof( _td ) ) )
                .ToString(),
        };
    }

    private static ElementClass GetHoverableStyles( bool hoverable, string slot )
    {
        return hoverable switch
        {
            true => ElementClass.Empty()
                .Add( "cursor-default", when: slot is nameof( _tr ) )
                .Add( "group-aria-[selected=false]:group-hover:bg-default-100/70", when: slot is nameof( _td ) ),

            _ => ElementClass.Empty()
        };
    }
}
