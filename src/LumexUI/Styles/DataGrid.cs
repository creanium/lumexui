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

            Table = ElementClass.Empty()
                .Add( _table )
                .ToString(),

            Thead = ElementClass.Empty()
                .Add( _tHead )
                .ToString(),

            Tr = ElementClass.Empty()
                .Add( _tr )
                .ToString(),

            Th = ElementClass.Empty()
                .Add( _th )
                .ToString(),

            Td = ElementClass.Empty()
                .Add( _td )
                .ToString(),
        };
    }
}
