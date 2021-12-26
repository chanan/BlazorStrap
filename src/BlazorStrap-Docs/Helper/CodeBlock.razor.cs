using ColorCode;
using Markdig;
using Markdig.SyntaxHighlighting;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BlazorStrap_Docs.Helper
{ 
    public sealed partial class CodeBlock : ComponentBase
    {
        [Inject] private HttpClient? HttpClient { get; set; }
        [Parameter] public string? Source { get; set; }
        private MarkupString _code = new MarkupString();

        private MarkupString _markup = new MarkupString();

        private static MarkdownPipeline Pipeline => new MarkdownPipelineBuilder()
            .UseBootstrap()
            .UseSyntaxHighlighting(new DefaultStyleSheet())
            .Build();


        protected override async Task OnParametersSetAsync()
        {
            if (Source == null || HttpClient == null) return;
            using var response = await HttpClient.GetAsync(Source + "?" + Guid.NewGuid().ToString());
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
            html = "```html\n" + html + "\n```";
            if(!string.IsNullOrEmpty(code))
                code = "```C#\n" + code + "\n```";
            _markup = new MarkupString(Markdown.ToHtml(html, Pipeline));
            _code = new MarkupString(Markdown.ToHtml(code, Pipeline));

            await base.OnParametersSetAsync();
        }
        
    }
}
