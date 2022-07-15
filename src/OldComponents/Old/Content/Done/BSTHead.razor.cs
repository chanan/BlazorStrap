using BlazorComponentUtilities;
using BlazorStrap.Components.Base;

namespace BlazorStrap
{
    public partial class BSTHead : LayoutBase
    {
        private string? ClassBuilder => new CssBuilder()
          
          .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
          .AddClass(Class, !string.IsNullOrEmpty(Class))
          .Build().ToNullString();
    }
}
