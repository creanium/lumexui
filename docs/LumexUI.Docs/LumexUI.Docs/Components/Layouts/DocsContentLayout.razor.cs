namespace LumexUI.Docs.Components;

public partial class DocsContentLayout
{
    private string? _title;
    private string? _section;
    private string? _description;

    private readonly List<DocsSection> _sections = [];

    public void SetHeader(
        string title,
        string section,
        string description )
    {
        _title = title;
        _section = section;
        _description = description;

        StateHasChanged();
    }

    public void AddSection( DocsSection section )
    {
        _sections.Add( section );
    }
}
