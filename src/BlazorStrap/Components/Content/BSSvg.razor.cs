using BlazorComponentUtilities;
using BlazorStrap.Utilities;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BlazorStrap
{
    public partial class BSSvg : BlazorStrapBase
    {
        [Inject] public ISvgLoader SvgLoader { get; set; }
        [Parameter] public Align Align { get; set; }
        [Parameter] public bool IsFluid { get; set; } 
        [Parameter] public bool IsRounded { get; set; }
        [Parameter] public bool IsThumbnail { get; set; }
        [Parameter] public string? Source { get; set; }
        [CascadingParameter] public BSFigure? Figure { get; set; }
        private MarkupString Markup { get; set; }

        private string? ClassBuilder => new CssBuilder()
            .AddClass("img-fluid", IsFluid)
            .AddClass("img-thumbnail", IsThumbnail)
            .AddClass("rounded", IsRounded)
            .AddClass("float-start", Align == Align.Start)
            .AddClass("float-end", Align == Align.End)
            .AddClass("mx-auto d-block", Align == Align.Center)
            .AddClass("figure-img", Figure != null)
            .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
            .AddClass(Class, !string.IsNullOrEmpty(Class))
            .Build().ToNullString();


        protected override async Task OnInitializedAsync()
        {
            if (!string.IsNullOrWhiteSpace(Source))
            {
                Markup = AddClass(await SvgLoader.LoadSvg(Source));
            }
        }

        private MarkupString AddClass(MarkupString svg)
        {
            var svgData = svg.ToString();
            var svgLine = Regex.Match(svg.ToString(),@"<svg(.*?)>" , RegexOptions.Singleline).Value;
            svgData = svgData.Replace(svgLine, "");
            var classIndex = svgLine.IndexOf("class=", StringComparison.Ordinal);
            if (classIndex > -1)
                svgLine = svgLine.Replace("class=", $"class={ClassBuilder} ");
            else
            {
                svgLine = svgLine.Replace("<svg", $"<svg class=\"{ClassBuilder}\" ");
            }
            return new MarkupString(svgLine + svgData);
        }
    }
}