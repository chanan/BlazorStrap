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

namespace BlazorStrap_Docs
{ 
    public sealed partial class MDOut : ComponentBase
    {
        [Inject] private HttpClient? HttpClient { get; set; }
        [Parameter] public string? Source { get; set; }
        private MarkupString _code = new MarkupString();

        private MarkupString _markup = new MarkupString();

        private static MarkdownPipeline Pipeline => new MarkdownPipelineBuilder()
            .UseBootstrap()
            .Build();


        protected override async Task OnParametersSetAsync()
        {
            if (Source == null || HttpClient == null) return;
            using var response = await HttpClient.GetAsync(Source + "?" + Guid.NewGuid().ToString());
            var markdown = await response.Content.ReadAsStringAsync();
            _markup = new MarkupString(Markdown.ToHtml(markdown, Pipeline));
            await base.OnParametersSetAsync();
        }
    }
}
