using BlazorComponentUtilities;
using BlazorStrap.Components.Base;

namespace BlazorStrap
{
    public partial class BSFigure : LayoutBase
    {
        internal string? ClassBuilder => new CssBuilder("figure")
          .Build().ToNullString();
    }
}
