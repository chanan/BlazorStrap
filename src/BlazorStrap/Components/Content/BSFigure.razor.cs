using BlazorComponentUtilities;

namespace BlazorStrap
{
    public partial class BSFigure : BlazorStrapBase
    {
        internal string? ClassBuilder => new CssBuilder("figure")
          .Build().ToNullString();
    }
}
