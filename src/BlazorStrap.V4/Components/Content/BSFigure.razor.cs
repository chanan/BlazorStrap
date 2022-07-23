using BlazorComponentUtilities;
using BlazorStrap.Extensions;
using BlazorStrap.Shared.Components.Content;

namespace BlazorStrap.V4
{
    public partial class BSFigure : BSFigureBase
    {
        protected override string? LayoutClass => LayoutClassBuilder.Build(this);

        protected override string? ClassBuilder => new CssBuilder("figure")
                .Build().ToNullString();
    }
}