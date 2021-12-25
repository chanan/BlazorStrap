using BlazorComponentUtilities;

namespace BlazorStrap
{
    public partial class BSFigureCaption : BlazorStrapBase
    {
        private string? ClassBuilder => new CssBuilder("figure")
          .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
          .AddClass(Class, !string.IsNullOrEmpty(Class))
          .Build().ToNullString();
    }
}
