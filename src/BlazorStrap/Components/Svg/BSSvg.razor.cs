using BlazorComponentUtilities;
using BlazorStrap.Extensions;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorStrap
{
    public partial class BSSvg
    {
        protected MarkupString Markup { get; set; }
        protected string Classname =>
        new CssBuilder("svg")
        .AddClass(Class)
        .Build();

        [Inject] public ISvgLoader SvgLoader { get; set; }

        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }
        [Parameter] public string Class { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public string Src { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if(!string.IsNullOrWhiteSpace(Src))
            {
                Markup = await SvgLoader.LoadSvg(Src);
            }
        }

    }
}
