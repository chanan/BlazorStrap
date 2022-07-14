using BlazorComponentUtilities;
using BlazorStrap.Components.Base;
using BlazorStrap.Utilities;
using Microsoft.AspNetCore.Components;
using System.Text.RegularExpressions;

namespace BlazorStrap
{
    public partial class BSSvg : LayoutBase
    {
        // ReSharper disable once NullableWarningSuppressionIsUsed
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