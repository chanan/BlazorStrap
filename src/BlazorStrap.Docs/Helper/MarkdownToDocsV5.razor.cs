using System.Net;
using System.Text.RegularExpressions;
using Markdig;
using Markdig.SyntaxHighlighting;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;

namespace BlazorStrap_Docs.Helper;

public partial class MarkdownToDocsV5 : ComponentBase, IDisposable
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
    private Dictionary<string, string?> _files = new Dictionary<string, string?>();

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

    protected override async Task OnParametersSetAsync()
    {
        if (!_initialized || _lastSource != Source)
        {
            NavigationManager.LocationChanged += NavigationManagerOnLocationChanged;
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

    private void NavigationManagerOnLocationChanged(object? sender, LocationChangedEventArgs e)
    {
        _files.Clear();
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
                if (typeName.Contains(";"))
                {
                    var parts = typeName.Split(";");
                    sample.Class = parts[1];
                    typeName = parts[0];
                }

                sample.Component = StringToType($"{namespaceRoot}.{typeName}");
                sample.IndexPath = string.Join("/", typeName.Split(".").SkipLast(1)) + "/" + "index.txt";

                //Load the index file if it's not already loaded
                if (_files.ContainsKey(sample.IndexPath))
                {
                    sample.Index = _files[sample.IndexPath];
                }
                else
                {
                    using var httpClient = new HttpClient() { BaseAddress = new Uri(NavigationManager.BaseUri) };
                    using var response = await httpClient.GetAsync("docs/Samples/" + sample.IndexPath + "?" + Guid.NewGuid().ToString().Replace("-", ""));
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        _files.TryAdd(sample.IndexPath, await response.Content.ReadAsStringAsync());
                    }
                }

                //Parse the index file to get the css and code files related to the sample
                sample.Index = _files[sample.IndexPath];
                var lines = sample.Index.Split("\n");
                sample.Index = lines.Where(line => line.Contains(sample.Component.Name)).Aggregate("", (current, line) => current + (line + ";"));
                sample.Index = sample.Index.Replace("\r", "").Replace("\n", "");
                sample.Index = Regex.Replace(sample.Index, @"[^a-zA-Z0-9;:.,\s]", "");

                if (sample.Index.Contains(';'))
                {
                    var items = sample.Index.Split(";");
                    var path = sample.IndexPath.Replace("/index.txt", "");
                    sample.HasCss = items.Any(x => x.EndsWith(".css.md"));
                    sample.HasCode = items.Any(x => x.EndsWith(".cs.md"));
                    sample.CssFile = $"{path}/{items.FirstOrDefault(x => x.EndsWith(".css.md"))}";
                    sample.CodeFile = $"{path}/{items.FirstOrDefault(x => x.EndsWith(".cs.md"))}";
                    sample.MarkupFile = $"{path}/{items.FirstOrDefault(x => x.EndsWith(".razor.md"))}";
                    if (sample.HasCode) _files.TryAdd(sample.CodeFile, null);
                    if (sample.HasCss) _files.TryAdd(sample.CssFile, null);
                    _files.TryAdd(sample.MarkupFile, null);
                }


                var content = source.Substring(0, left + match.ToString().Length);
                // Removes the content from the source
                source = source.Substring(left + match.ToString().Length);
                content = content.Replace(match.ToString(), "");
                sample.Content = ToMarkupString(content);
                result.Add(sample);
            }
        }

        // Adds the last part of the content after the last sample
        result.Add(new Sample { Content = ToMarkupString(source) });
        return result;
    }

    public async Task<string?> LoadFile(string filePath)
    {
        if (_files.TryGetValue(filePath, out var value)) return value;
        using var httpClient = new HttpClient() { BaseAddress = new Uri(NavigationManager.BaseUri) };
        using var response = await httpClient.GetAsync("docs/Samples/" + filePath + "?" + Guid.NewGuid().ToString().Replace("-", ""));
        if (response.StatusCode == HttpStatusCode.OK)
        {
            _files.Add(filePath, await response.Content.ReadAsStringAsync());
        }

        return _files[filePath];
    }

    public static MarkupString ToMarkupString(string sample)
    {
        return new MarkupString(Markdown.ToHtml(sample, Pipeline));
    }

    public void Dispose()
    {
        NavigationManager.LocationChanged -= NavigationManagerOnLocationChanged;
    }
}