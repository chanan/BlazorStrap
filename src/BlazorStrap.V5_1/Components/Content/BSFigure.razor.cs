using BlazorComponentUtilities;
using BlazorStrap.Extensions;
using BlazorStrap.Interfaces;
using BlazorStrap.Shared.Components.Content;

namespace BlazorStrap.V5_1
{
    public partial class BSFigure : BSFigureBase, IBSFigure
    {
        protected override string? LayoutClass => LayoutClassBuilder.Build(this);

        protected override string? ClassBuilder => new CssBuilder("figure")
                .Build().ToNullString();
    }
}