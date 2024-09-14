using System.Net;
using System.Text.RegularExpressions;
using Markdig;
using Markdig.SyntaxHighlighting;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace BlazorStrap_Docs.Helper;

public class SampleLoader : ComponentBase
{
    [Parameter] public Dictionary<string, string?> Files { get; set; }
    [Parameter] public Uri BaseAddress { get; set; }
    [Parameter] public string? FilePath { get; set; }
    [Parameter] public FileType FileType { get; set; }
    
    private string _lastFilePath;
    
    private static MarkdownPipeline Pipeline => new MarkdownPipelineBuilder()
        .UseBootstrap()
        .UseSyntaxHighlighting(new DefaultStyleSheet())
        .Build();

    protected override async Task OnParametersSetAsync()
    {
        if(_lastFilePath != FilePath)
        {
            await LoadFileAsync(FilePath);
            _lastFilePath = FilePath;
        }
    }
    
    private async Task LoadFileAsync(string filePath)
    {
        if (!Files.TryGetValue(filePath, out var value)) return;
        if (value is not null) return;
        
        using var httpClient = new HttpClient() { BaseAddress = BaseAddress };
        using var response = await httpClient.GetAsync("docs/Samples/" + filePath + "?" + Guid.NewGuid().ToString().Replace("-", ""));
        if (response.StatusCode == HttpStatusCode.OK)
        {
            Files[filePath] = await response.Content.ReadAsStringAsync();
        }
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        var context = FileType switch
        {
            FileType.Markup => ToSourceMarkupString(RemoveCode(Files[FilePath])),
            FileType.Code => ToCodeMarkupString(Files[FilePath]),
            FileType.Css => ToCssMarkupString(Files[FilePath]),
            _ => ToCodeMarkupString(RemoveMarkup(Files[FilePath]))
        };
        builder.AddContent(0, context);
    }
    
    public static MarkupString ToSourceMarkupString(string? sample)
    {
        if(sample is null) return new MarkupString("");
        sample = Regex.Replace(sample,@"<!--\\\\-->(.*?)<!--//-->", "" , RegexOptions.Singleline);
        return new MarkupString(Markdown.ToHtml("```html\n" + sample + "\n```",Pipeline));
    }

    public static MarkupString ToCodeMarkupString(string? sample)
    {
        if(sample is null) return new MarkupString("");
        sample = Regex.Replace(sample, @"<!--\\\\-->(.*?)<!--//-->", "", RegexOptions.Singleline);
        //trim all lines that are empty
        sample = string.Join("\n", sample.Split("\n").Where(x => !string.IsNullOrWhiteSpace(x)));
        return new MarkupString(Markdown.ToHtml("```C#\n" + sample + "\n```", Pipeline));
    }

    public static MarkupString ToCssMarkupString(string? sample)
    {
        if(sample is null) return new MarkupString("");
        return new MarkupString(Markdown.ToHtml("```css\n" + sample + "\n```", Pipeline));
    }
    
    public static string RemoveCode(string? content)
    {
        if(content == null) return "";
        return Regex.Replace(content, @"@code\s*\{((?<Curly>\{)|(?<-Curly>\})|[^{}]+)*(?(Curly)(?!))\}", "", RegexOptions.Singleline);
    }
    public static string RemoveMarkup(string? content)
    {
        if(content == null) return "";
        return Regex.Match(content,@"@code\s*\{((?<Curly>\{)|(?<-Curly>\})|[^{}]+)*(?(Curly)(?!))\}", RegexOptions.Singleline).Value;
    }
  
}