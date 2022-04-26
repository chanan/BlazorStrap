using ColorCode;
using Markdig;
using Markdig.SyntaxHighlighting;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BlazorStrap_Docs.Helper
{ 
    public sealed partial class CodeBlock : ComponentBase
    {
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Parameter] public string? Source { get; set; }
        [Parameter] public bool CSS { get; set; }
        private bool _hasMarkup = false;
        private bool _hasCss = false;
        private bool _hasCode = false;
        private MarkupString _code = new MarkupString();
        private MarkupString _css = new MarkupString();
        private MarkupString _markup = new MarkupString();

        private static MarkdownPipeline Pipeline => new MarkdownPipelineBuilder()
            .UseBootstrap()
            .UseSyntaxHighlighting(new DefaultStyleSheet())
            .Build();


        protected override async Task OnParametersSetAsync()
        {
            string css = "";
            using var httpClient = new HttpClient() { BaseAddress = new Uri(NavigationManager.BaseUri) };
            if (Source == null || httpClient == null) return;
            using var response = await httpClient.GetAsync(Source + ".md" + "?" + Guid.NewGuid().ToString());
            if (CSS)
            {
                using var cssResponse =
                    await httpClient.GetAsync(Source + ".razor.md" + "?" + Guid.NewGuid().ToString());
                if (cssResponse.StatusCode != HttpStatusCode.OK)
                    return;
                css = await cssResponse.Content.ReadAsStringAsync();
            }

            if (response.StatusCode != HttpStatusCode.OK)
                return;
           
            var markdown = await response.Content.ReadAsStringAsync();
            markdown = Regex.Replace(markdown,@"<!--\\\\-->(.*?)<!--//-->", "" , RegexOptions.Singleline);
            string html;
            
            var code = "";
            
            if (markdown.IndexOf("@code", StringComparison.Ordinal) != -1)
            {
                html = markdown.Substring(0, markdown.IndexOf("@code", StringComparison.Ordinal));;
                html = html.TrimEnd('\n', '\r');
                code = markdown.Substring(markdown.IndexOf("@code", StringComparison.Ordinal));
            }
            else
            {
                html = markdown;
                html = html.TrimEnd('\n', '\r');
            }
            if (!string.IsNullOrWhiteSpace(html))
                _hasMarkup = true;
            if (!string.IsNullOrWhiteSpace(code))
                _hasCode = true;
            if (!string.IsNullOrWhiteSpace(css))
                _hasCss = true;
            
            html = "```html\n" + html + "\n```";
            if(CSS)
                css = "```css\n" + css + "\n```";
            if(!string.IsNullOrEmpty(code))
                code = "```C#\n" + code + "\n```";
            
            _markup = new MarkupString(Markdown.ToHtml(html, Pipeline));
            _code = new MarkupString(Markdown.ToHtml(code, Pipeline));
            _css = new MarkupString(Markdown.ToHtml(css, Pipeline));
       
            await base.OnParametersSetAsync();
        }
        
    }
}
