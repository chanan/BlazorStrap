using Markdig;
using Markdig.SyntaxHighlighting;
using Microsoft.AspNetCore.Components;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Components.Rendering;
using BlazorStrap.V5;

namespace BlazorStrap_Docs.Helper
{ 
    public class MarkdownToComponent : ComponentBase
    {
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Parameter] public string? NamespaceRoot { get; set; }
        [Parameter] public string? WebRoot { get; set; }
        [Parameter] public string? DefaultClass { get; set; }
        [Parameter] public string? Source { get; set; }
        [Parameter] public RenderFragment? ChildContent { get; set; }
        private MarkupString _code = new MarkupString();
        private MarkupString _markup = new MarkupString();
        private bool _init;
        private string _rawData = "";
        private bool NotFound;
        private string _lastSource;
        private MatchCollection Matches { get; set; }
        private List<string>? Data { get; set; } = null;
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
       
            if (_init is false || _lastSource != Source)
            {
                if (Source == null ) return;
                using var httpClient = new HttpClient() { BaseAddress = new Uri(NavigationManager.BaseUri)};
                    using var response = await httpClient.GetAsync(Source + "?" + Guid.NewGuid().ToString().Replace("-",""));
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    _rawData = await response.Content.ReadAsStringAsync();
                    Matches = GetSamples(_rawData);
                    Data = GetBlocksBetweenSamples(Matches, _rawData);
                    //Probably not needed but to be safe
                    NotFound = false;
                }
                else
                {
                    Matches = null;
                    Data = new List<string>();
                    NotFound = true;
                }
                _lastSource = Source;
                _init = true;
            }
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            var hasCss = false;
            if (NotFound)
            {
                    builder.AddContent(0, ChildContent);
            }
            
            if (!_init || Data == null)
            {
                return;
            }

            if (_init)
            {
                var i = 0;
                var x = 0; // Builder counter
                foreach (var item in Data)
                {
                    var classList = DefaultClass;
                    builder.AddContent(x++, new MarkupString(Markdown.ToHtml(item, Pipeline)));
                    //fake add comp
                    if (i < Matches?.Count)
                    {
                        var typeName = Matches[i].ToString().Replace("/", ".").Replace("{{sample=", "").Replace("}}", "");
                        if (typeName.Contains(";"))
                        {
                            classList += " " + typeName.Substring(typeName.IndexOf(";") +1);
                            Console.WriteLine(classList);
                            typeName = typeName.Substring(0, typeName.IndexOf(";"));
                            if (classList.StartsWith(" "))
                                classList = classList.Substring(1);
                            if(classList.Contains(";CSS"))
                            {
                                hasCss = true;
                                classList = classList.Replace(";CSS", "");
                            }

                        }
                        var type = StringToType($"{NamespaceRoot}.{typeName}");
                        if (type != null)
                        {
                            builder.OpenElement(x++, "div");
                            builder.AddAttribute(x++, "class", classList);
                                builder.OpenComponent<BSNav>(x++);
                                builder.AddAttribute(x++, "IsTabs", true);
                                builder.AddAttribute(x++, "ChildContent", (RenderFragment)((tabBuilder) =>
                                {
                                    tabBuilder.AddContent(x++, CreateTab("Example", type, TabType.Example));
                                    tabBuilder.AddContent(x++, CreateTab("Code", typeof(CodeBlock), TabType.Code, $"{WebRoot}{typeName.Replace(".", "/")}"));
                                    if (hasCss)
                                        tabBuilder.AddContent(x++, CreateTab("CSS",typeof(CodeBlock), TabType.Css,$"{WebRoot}{typeName.Replace(".", "/")}"));
                                }));
                                builder.CloseComponent();
                            builder.CloseElement();
                            // builder.OpenComponent(x++, typeof(CodeBlock));
                            // builder.AddAttribute(x++, "Source", $"{WebRoot}{typeName.Replace(".", "/")}");
                            // builder.AddAttribute(x++, "CSS", hasCss);
                            // builder.CloseComponent();
                        }
                    }
                    ++i;
                    
                }
               
            }
        }

        public static RenderFragment CreateTab(string label, Type type, TabType tabType, string source = "")
        {
            var x = 0;
            return builder =>
            {
                builder.OpenComponent<BSNavItem>(x++);
                builder.AddAttribute(x++, "TabLabel", (RenderFragment)((tabLabelBuilder) =>
                    {
                    
                            tabLabelBuilder.AddContent(x++, label);
                     }));
                builder.AddAttribute(x++, "TabContent", (RenderFragment)((tabContentBuilder) =>
                {
                    tabContentBuilder.AddContent(0, tabType switch
                    {
                        TabType.Code => CodeTab(type, source),
                        TabType.Css => CssTab(type, source),
                        _ => ExampleTab(type)
                    });
                }));
                builder.CloseComponent();
            };
        }
        private static RenderFragment ExampleTab(Type type)
        {
            return builder =>
            {
                builder.OpenComponent(0, type);
                builder.CloseComponent();
            };
        }
        private static RenderFragment CodeTab(Type type, string source)
        {
            return builder =>
            {
                builder.OpenComponent(0, typeof(CodeBlock));
                builder.AddAttribute(1, "Source", source);
                builder.AddAttribute(2, "Css", false);
                builder.CloseComponent();
            };
        }
        private static RenderFragment CssTab(Type type, string source)
        {
            return builder =>
            {
                builder.OpenComponent(0, typeof(CodeBlock));
                builder.AddAttribute(1, "Source", source);
                builder.AddAttribute(2, "Css", true);
                builder.CloseComponent();
            };
        }
        
        public static MatchCollection GetSamples(string data)
        {
            return Regex.Matches(data, @"{{sample=(.*?)}}");
        }
        public static Type StringToType(string typeName)
        {
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                Type foundType = assembly.GetType(typeName);

                if (foundType != null)
                    return foundType;
            }
            return null;
        }
        public static List<string> GetBlocksBetweenSamples(MatchCollection matchs, string source)
        {
            var result = new List<string>();
            if (matchs.Count == 0)
            {
                result.Add(source);
                return result;
            }
            
            foreach (var match in matchs)
            {
                var left = source.IndexOf(match.ToString());
                if (left > 0)
                {
                    result.Add(source.Substring(0, left));
                    source = source.Substring(left).Replace(match.ToString(), "");
                }
            }
            result.Add(source);

            return result;
        }
   
    }
    public enum TabType
    {
        Example,
        Code,
        Css
    }
}
