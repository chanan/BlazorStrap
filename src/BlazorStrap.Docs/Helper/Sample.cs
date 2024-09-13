using Microsoft.AspNetCore.Components;

namespace BlazorStrap_Docs.Helper;

public class Sample
{
    public MarkupString Content { get; set; }
    public Type? Component { get; set; }
    public string? Class { get; set; }
    public string? Index { get; set; }
    public string? IndexPath { get; set; }
    public bool HasCss => Index.Split(";").Any(x => x.Contains(".css"));
    public bool HasCode => Index.Split(";").Any(x => x.Contains(".cs") && !x.Contains(".css"));
    public string? CssFile => Index.Split(";").FirstOrDefault(x => x.Contains(".css"));
    public string? CodeFile => Index.Split(";").FirstOrDefault(x => x.Contains(".cs") && !x.Contains(".css"));
    public string? MarkupFile => Index.Split(";").FirstOrDefault(x => x.Contains(".razor") && !x.Contains(".css") && !x.Contains(".cs"));        
        
}