using BlazorComponentUtilities;
using BlazorStrap.Extensions;
using BlazorStrap.Shared.Components.OffCanvas;

namespace BlazorStrap.V5
{
    public partial class BSOffCanvas : BSOffCanvasBase
    {
        protected override string? LayoutClass => LayoutClassBuilder.Build(this);

        protected override string? ClassBuilder => new CssBuilder("offcanvas")
                .AddClass($"offcanvas-{Placement.NameToLower().LeftRightToStartEnd()}")
                .AddClass("show", Shown)
                .AddClass($"bg-{Color.NameToLower()}", Color != BSColor.Default)
                .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
                .AddClass(Class, !string.IsNullOrEmpty(Class))
                .Build().ToNullString();

        protected override string? BodyClassBuilder => new CssBuilder("offcanvas-body")
                .AddClass(BodyClass)
                .Build().ToNullString();

        protected override string? HeaderClassBuilder => new CssBuilder("offcanvas-header")
                .AddClass(HeaderClass)
                .Build().ToNullString();
    }
}