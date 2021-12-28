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
        [Parameter] public bool IsPlaceholder { get; set; }
        [Parameter] public string? Source { get; set; }
        [CascadingParameter] public BSFigure? Figure { get; set; }
        private int PlaceHolderHeight { get; set; } = 100;
        private int PlaceHolderWidth { get; set; } = 100;
        private string PlaceHolderText { get; set; } = "";
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

        protected override void OnParametersSet()
        {
            if (Source == null) return;
            if (!IsPlaceholder && !Source.ToLower().Contains("x")) return;
            var data = Source.Split("x");
            PlaceHolderHeight = Convert.ToInt32(data[1]);
            PlaceHolderWidth = Convert.ToInt32(data[0]);
            PlaceHolderText = $"{data[0]}x{data[1]}";
            if (data.Length > 2)
                PlaceHolderText = data[2];
        }
    }
}
