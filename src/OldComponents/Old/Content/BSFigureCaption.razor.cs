using BlazorComponentUtilities;
using BlazorStrap.Components.Base;

namespace BlazorStrap
{
    public partial class BSFigureCaption : LayoutBase
    {
        private string? ClassBuilder => new CssBuilder("figure-caption")
          .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
          .AddClass(Class, !string.IsNullOrEmpty(Class))
          .Build().ToNullString();
    }
}
