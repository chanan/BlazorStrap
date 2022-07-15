using BlazorStrap.Interfaces;
using BlazorStrap.Utilities;
using Microsoft.AspNetCore.Components;
using System.Text.RegularExpressions;

namespace BlazorStrap.Shared.Components.Content
{
    public abstract class BSSvgBase : BlazorStrapBase
    {
        [Inject] public ISvgLoader SvgLoader { get; set; } = null!;

        /// <summary>
        /// SVG alignment
        /// </summary>
        [Parameter] public Align Align { get; set; }

        /// <summary>
        /// Adds the <c>img-fluid</c> class.
        /// </summary>
        [Parameter] public bool IsFluid { get; set; }

        /// <summary>
        /// Adds the <c>rounded</c> class.
        /// </summary>
        [Parameter] public bool IsRounded { get; set; }

        /// <summary>
        /// Adds the <c>img-thumbnail</c> class.
        /// </summary>
        [Parameter] public bool IsThumbnail { get; set; }

        /// <summary>
        /// SVG source
        /// </summary>
        [Parameter] public string? Source { get; set; }

        [CascadingParameter] public IBSFigure? Figure { get; set; }
        protected abstract string? LayoutClass { get; }
        protected abstract string? ClassBuilder { get; }
        protected MarkupString Markup { get; set; }

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
            var svgLine = Regex.Match(svg.ToString(), @"<svg(.*?)>", RegexOptions.Singleline).Value;
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
