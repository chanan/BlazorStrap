using BlazorComponentUtilities;
using BlazorStrap.Extensions;
using BlazorStrap.Shared.Components.Content;

namespace BlazorStrap.V5
{
    public partial class BSImage : BSImageBase
    {
        protected override string? LayoutClass => LayoutClassBuilder.Build(this);

        protected override string? ClassBuilder => new CssBuilder()
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
