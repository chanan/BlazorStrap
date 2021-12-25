using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap
{
    public partial class BSImage : BlazorStrapBase
    {
        [Parameter] public Align Align { get; set; }
        [Parameter] public bool IsFluid { get; set; }
        [Parameter] public bool IsRounded { get; set; }
        [Parameter] public bool IsThumbnail { get; set; }
        [Parameter] public string Source { get; set; }
        [CascadingParameter] public BSFigure? Figure { get; set; }

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
    }
}
