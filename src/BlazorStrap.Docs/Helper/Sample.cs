using Microsoft.AspNetCore.Components;

namespace BlazorStrap_Docs.Helper;

public class Sample
{
    public MarkupString Content { get; set; }
    public Type? Component { get; set; }
    public string? Class { get; set; }
    public string? Index { get; set; }
    public string? IndexPath { get; set; }
    public bool HasCss { get; set; }
    public bool HasCode { get; set; }
    public string? CssFile { get; set; }
    public string? CodeFile { get; set; }
    public string? MarkupFile { get; set; }
}