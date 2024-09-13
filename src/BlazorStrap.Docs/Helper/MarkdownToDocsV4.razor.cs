using System.Net;
using System.Text.RegularExpressions;
using Markdig;
using Markdig.SyntaxHighlighting;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap_Docs.Helper;

public partial class MarkdownToDocsV4 : ComponentBase
{
    [Inject] public NavigationManager NavigationManager { get; set; }
    [Parameter] public string? NamespaceRoot { get; set; }
    [Parameter] public string? WebRoot { get; set; }
    [Parameter] public string? DefaultClass { get; set; }
    [Parameter] public string? Source { get; set; }
    [Parameter] public RenderFragment? ChildContent { get; set; }
    private string _lastSource;
    private bool _initialized;
    private bool _notFound;
    private List<Sample>? Samples { get; set; } = null;
    private string _indexPath = "index.txt";
    private Dictionary<string,string> _files = new Dictionary<string, string>();
    private static MarkdownPipeline Pipeline => new MarkdownPipelineBuilder()
        .UseBootstrap()
        .UseEmojiAndSmiley()
        .UseAdvancedExtensions()
        .UseCustomContainers()
        .UseAutoLinks()
        .UseSyntaxHighlighting()
        .UseSoftlineBreakAsHardlineBreak()
        .UsePipeTables()
        .UseCitations()
        .Build();

    private static MarkdownPipeline SourcePipeline => new MarkdownPipelineBuilder()
        .UseBootstrap()
        .UseSyntaxHighlighting(new DefaultStyleSheet())
        .Build();

    protected override async Task OnParametersSetAsync()
    {
        if (!_initialized || _lastSource != Source)
        {
            if (Source == null) return;
            using var httpClient = new HttpClient() { BaseAddress = new Uri(NavigationManager.BaseUri) };
            using var response = await httpClient.GetAsync(Source + "?" + Guid.NewGuid().ToString().Replace("-", ""));
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var rawData = await response.Content.ReadAsStringAsync();
                Samples = await GetContentBetweenSamplesAsync(GetSamples(rawData), rawData, NamespaceRoot);
                _initialized = true;
                _lastSource = Source;
            }
            else
            {
                Samples = [];
                _notFound = true;
            }
        }
    }

    public static MatchCollection GetSamples(string data)
    {
        return Regex.Matches(data, @"{{sample=(.*?)}}");
    }

    public static Type StringToType(string typeName)
    {
        return AppDomain.CurrentDomain.GetAssemblies().Select(assembly => assembly.GetType(typeName)).OfType<Type>().FirstOrDefault();
    }

    public async Task<List<Sample>> GetContentBetweenSamplesAsync(MatchCollection matchs, string source, string namespaceRoot)
    {
        var result = new List<Sample>();
        if (matchs.Count == 0)
        {
            result.Add(new Sample { Content = ToMarkupString(source) });
            return result;
        }
        // Adds the first part of the content before the first sample
        foreach (var match in matchs)
        {
            var left = source.IndexOf(match.ToString(), StringComparison.Ordinal);
            if (left > 0)
            {
                var sample = new Sample();
                var typeName = match.ToString().Replace("/", ".").Replace("{{sample=", "").Replace("}}", "");
                if(typeName.Contains(";"))
                {
                    var parts = typeName.Split(";");
                    sample.Class = parts[1];
                    typeName = parts[0];
                }

                sample.IndexPath = string.Join("/", typeName.Split(".").SkipLast(1)) + "/" + "index.txt";
                if(result.Any(x => x.IndexPath == sample.IndexPath))
                {
                    sample.Index = result.First(x => x.IndexPath == sample.IndexPath).Index;
                }
                else
                {
                    using var httpClient = new HttpClient() { BaseAddress = new Uri(NavigationManager.BaseUri) };
                    using var response = await httpClient.GetAsync("/docs/Samples/" + sample.IndexPath + "?" + Guid.NewGuid().ToString().Replace("-", ""));
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        sample.Index = await response.Content.ReadAsStringAsync();
                    }
                }

                sample.Component = StringToType($"{namespaceRoot}.{typeName}");
                var content = source.Substring(0, left + match.ToString().Length);
                source = source.Substring(left + match.ToString().Length);
                content = content.Replace(match.ToString(), "");
                sample.Content = ToMarkupString(content);
                result.Add(sample);
                
                // Removes the content from the source
            }
        }
    
        
        // Trim sample indexs to only related items
        foreach (var sample in result)
        {
            if (sample.Index != null && sample.Component != null)
            {
                var lines = sample.Index.Split("\n");
                sample.Index = lines.Where(line => line.Contains(sample.Component.Name)).Aggregate("", (current, line) => current + (line + ";"));
                sample.Index = sample.Index.Replace("\r", "").Replace("\n", "");
                sample.Index = Regex.Replace(sample.Index, @"[^a-zA-Z0-9;:.,\s]", "");
            }
        }
        
        //Finally load and cache all the files

        foreach (var sample in result)
        {
            using var httpClient = new HttpClient() { BaseAddress = new Uri(NavigationManager.BaseUri) };
            var lines = sample.Index.Split(";");
            var path = sample.IndexPath.Replace("/index.txt", "");
            foreach (var line in lines)
            {
                if (!_files.ContainsKey(line))
                {
                    using var response = await httpClient.GetAsync("/docs/Samples/" + path + "/" + line + "/?" + Guid.NewGuid().ToString().Replace("-", ""));
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        _files.Add(line, await response.Content.ReadAsStringAsync());
                    }
                }
            }
        }
    
        // Adds the last part of the content after the last sample
        result.Add(new Sample { Content = ToMarkupString(source) });
        return result;
    }

    public static MarkupString ToMarkupString(string sample)
    {
        return new MarkupString(Markdown.ToHtml(sample, Pipeline));
    }
    
    public static string RemoveCode(string content)
    {
        content = Regex.Replace(content, @"^@using.*$\n", "", RegexOptions.Multiline);
        content = Regex.Replace(content, @"^@inject.*$\n", "", RegexOptions.Multiline);
        return Regex.Replace(content, @"@code\s*\{((?<Curly>\{)|(?<-Curly>\})|[^{}]+)*(?(Curly)(?!))\}", "", RegexOptions.Singleline);
    }
    public static string RemoveMarkup(string content)
    {   
        return Regex.Match(content,@"@code\s*\{((?<Curly>\{)|(?<-Curly>\})|[^{}]+)*(?(Curly)(?!))\}", RegexOptions.Singleline).Value;
    }
    public static MarkupString ToSourceMarkupString(string sample)
    {
        sample = Regex.Replace(sample,@"<!--\\\\-->(.*?)<!--//-->", "" , RegexOptions.Singleline);
        return new MarkupString(Markdown.ToHtml("```html\n" + sample + "\n```",SourcePipeline));
    }

    public static MarkupString ToCodeMarkupString(string sample)
    {
        sample = Regex.Replace(sample, @"<!--\\\\-->(.*?)<!--//-->", "", RegexOptions.Singleline);
        //trim all lines that are empty
        sample = string.Join("\n", sample.Split("\n").Where(x => !string.IsNullOrWhiteSpace(x)));
        return new MarkupString(Markdown.ToHtml("```C#\n" + sample + "\n```", SourcePipeline));
    }

    public static MarkupString ToCssMarkupString(string sample)
    {
        return new MarkupString(Markdown.ToHtml("```css\n" + sample + "\n```", SourcePipeline));
    }
}