using BlazorComponentUtilities;

namespace BlazorStrap
{
    public partial class BSPicture : BlazorStrapBase
    {
        internal string? ClassBuilder => new CssBuilder("figure")
          .AddClass(LayoutClass, !String.IsNullOrEmpty(LayoutClass))
          .AddClass(Class, !String.IsNullOrEmpty(Class))
          .Build().ToNullString();
    }
}
